using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using StealthAPI;

namespace DrabadanCoreLib.Core
{
    public class ScriptActionExecuter
    {
        public static Action<string> Messanger { get; set; }
        public static Stealth StealthClient
        {
            get
            {
                try
                {
                    return Stealth.Client;
                }catch(Exception ex)
                {
                    Messanger?.Invoke($"[Error message] Stealth error message! {ex.Message}");
                    return default(Stealth);
                }
            }
        }
    
        private const int AverageLagConst = 1000;
        private const int RetriesCount = 10;

        private static async Task<bool> ValidatorActionsAsync(List<Func<bool>> validationActions)
        {
            bool result = true;

            await Task.Run(() =>
            {
                validationActions?.ForEach((a) =>
                {
                    try
                    {
                        result = a.Invoke();
                    }catch(SocketException)
                    {
                        Messanger?.Invoke("[ActionValidator Error message] Stealth not found!");
                        result = false;
                    }catch(Exception ex)
                    {
                        Messanger?.Invoke(ex.Message);
                        result = false;
                    }
                });
            });
            return result;
        }

        private static readonly List<Func<bool>> SimpleCallValidationActionsList = new List<Func<bool>>
        {
            () => StealthClient.GetConnectedStatus()
        };


        protected static async Task<T> ScriptApiCallAsync<T>(Func<T> scriptAction, int delay = 0, [CallerMemberName] string caller = "ScriptAction")
        {
            bool callIsValid = await ValidatorActionsAsync(SimpleCallValidationActionsList);
            if (callIsValid)
            {
                if(delay > 0)
                    await Task.Delay(delay);
                T result = await Task.Run(() => { return scriptAction(); });
                return result;
            }
            else
                Messanger?.Invoke($"[Error message] {caller} call is invalid.");

            return default(T);
        }

        protected static async Task ScriptApiCallAsync(Action scriptAction,int delay = 0, [CallerMemberName] string caller = "ScriptAction")
        {
            bool callIsValid = await ValidatorActionsAsync(SimpleCallValidationActionsList);
            if (callIsValid)
            {
                if (delay > 0)
                    await Task.Delay(delay);
                await Task.Run(() => scriptAction());
            }
            else
                Messanger?.Invoke($"[Error message] {caller} call is invalid.");
        }

        private static EventInfo EventResolver(Type evArgsType)
        {
            var eventList = typeof(Stealth).GetEvents().ToList();
            return (from evnt in eventList
                let evntArgsList = evnt.EventHandlerType.GenericTypeArguments
                from arg in evntArgsList
                where arg == evArgsType
                select evnt).FirstOrDefault();
        }

        protected static async Task<bool> ScriptApiCallAsync<T>(Action scriptAction, EventHandler<T> evHandler, CancellationTokenSource canceller, int maxDelay = 3000)
            where T : EventArgs
        {
            EventInfo ev = EventResolver(typeof(T));
            if (ev == null)
                throw new ArgumentException($"{typeof(T).Name} is not supported by Stealth!");

            bool restartLocker = false;
            int restartCounter = 0;

            Delegate handler = null;

            bool actionResult = false;
            actionResult = await Task.Run(async () =>
            {
                try
                {
                    handler = Delegate.CreateDelegate(ev.EventHandlerType, evHandler.Target, evHandler.Method);
                    ev.AddEventHandler(Stealth.Client, handler);

                    Stealth.Client.ClilocSpeech += (sender, e) =>
                    {
                        if (e.Text.Contains("perform another action"))
                        {
                            restartLocker = true;
                        }
                    };

                    do
                    {
                        if (restartCounter < RetriesCount)
                            restartCounter++;
                        else
                            return false;

                        scriptAction();
                        await Task.Delay(AverageLagConst);
                        await Task.Delay(maxDelay, canceller.Token);
                    } while (!restartLocker);
                }
                catch (TaskCanceledException)
                {
                    //removing current handlers                
                    ev.RemoveEventHandler(Stealth.Client, handler);

                    return true;
                }
                catch (SocketException)
                {
                    ev.RemoveEventHandler(Stealth.Client, handler);
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                return false;
            });

            return actionResult;
        }
    }
}