using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace Core.Common.Extensions
{
    public static class MefExtensions
    {
        private static readonly List<string> ComposedEportedValues = new List<string>();

        public static void AddComposedValue(this CompositionContainer container, string key, string value)
        {
            if (ComposedEportedValues.Contains(value)) return;

            ComposedEportedValues.Add(value);
            container.ComposeExportedValue(key, value);
        }

        public static void RemoveComposedValue(this CompositionContainer container, string key)
        {
            if (!ComposedEportedValues.Contains(key)) return;

            ComposedEportedValues.Remove(key);
        }

        public static object GetExportedValueByType(this CompositionContainer container, Type type)
        {
            foreach (var partDef in container.Catalog.Parts)
            {
                foreach (var exportDef in partDef.ExportDefinitions)
                {
                    if (exportDef.ContractName != type.FullName) continue;
                    var contract = AttributedModelServices.GetContractName(type);
                    var definition = new ContractBasedImportDefinition(contract, contract, null, ImportCardinality.ExactlyOne,
                        false, false, CreationPolicy.Any);
                    var firstOrDefault = container.GetExports(definition).FirstOrDefault();
                    if (firstOrDefault != null)
                        return firstOrDefault.Value;
                }
            }

            return null;
        }

        public static IEnumerable<object> GetExportedValuesByType(this CompositionContainer container, 
            Type type)
        {
            foreach (var partDef in container.Catalog.Parts)
            {
                foreach (var exportDef in partDef.ExportDefinitions)
                {
                    if (exportDef.ContractName != type.FullName) continue;
                    var contract = AttributedModelServices.GetContractName(type);
                    var definition = new ContractBasedImportDefinition(contract, contract, null, ImportCardinality.ExactlyOne,
                        false, false, CreationPolicy.Any);
                    return container.GetExports(definition);
                }
            }

            return new List<object>();
        }

        public static T GetExportedValue<T>(this CompositionContainer container,
            Func<IDictionary<string, object>, bool> predicate)
        {
            foreach (var partDef in container.Catalog.Parts)
            {
                foreach (var exportDef in partDef.ExportDefinitions)
                {
                    if (exportDef.ContractName != typeof (T).FullName) continue;
                    if (predicate(exportDef.Metadata))
                        return (T)partDef.CreatePart().GetExportedValue(exportDef);
                }
            }
            return default(T);
        }

        public static T GetExportedValueByType<T>(this CompositionContainer container, string type)
        {
            foreach (var partDef in container.Catalog.Parts)
            {
                foreach (var exportDef in partDef.ExportDefinitions)
                {
                    if (exportDef.ContractName == type)
                        return (T)partDef.CreatePart().GetExportedValue(exportDef);
                }
            }
            return default(T);
        }
    }
}
