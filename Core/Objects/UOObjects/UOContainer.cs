using DrabadanCoreLib.Core.ScriptActions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.Objects.UOObjects
{
    public class UoContainer : UoItem
    {
        public UoContainer() { }
        private UoContainer(uint id) : base(id)
        {

        }

        public static async Task<UoContainer> ConstructorProxy(uint id)
        {
            bool isContainer = await FindTypeActions.IsContainerAsync(id);
            if (isContainer)
                return new UoContainer(id);

            return default(UoContainer);
        }

        public UoObjectProperty<List<UoItem>> Contents { get; protected set; } = new UoObjectProperty<List<UoItem>>(new List<UoItem>(), true);

        public override async Task UpdateSelfAsync()
        {
            await base.UpdateSelfAsync();
            var contents = await ContainerOpener.OpenContainerGetContentsAsync(Id.Value);

            List<UoItem> resultList = new List<UoItem>();
            if (contents != null)
                foreach (var id in contents)
                {
                    UoItem item = new UoItem(id);
                    await item.UpdateSelfAsync();
                    resultList.Add(item);
                }

            Contents.Value = resultList;
            Contents.CurrentState = UoPropertyStateEnum.Initialized;
        }
    }

    public static class UoContainerExtensions
    {
        /// <summary>
        /// extension method that opens container and returns it's contents as list of UoItem
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static async Task<List<UoItem>> OpenAsync(this UoContainer container)
        {
            var contents = await ContainerOpener.OpenContainerGetContentsAsync(container.Id.Value);

            List<UoItem> resultList = new List<UoItem>();
            List<UoContainer> containers = new List<UoContainer>();
            if (contents != null)
                foreach (var id in contents)
                {
                    UoItem item = new UoItem(id);
                    resultList.Add(item);
                }

            return resultList;
        }
    }
}