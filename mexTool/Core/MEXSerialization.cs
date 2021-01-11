using HSDRaw;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace mexTool.Core
{
    public class MEXTypeInspector : TypeInspectorSkeleton
    {
        private readonly ITypeInspector _innerTypeDescriptor;

        public MEXTypeInspector(ITypeInspector innerTypeDescriptor)
        {
            _innerTypeDescriptor = innerTypeDescriptor;
        }

        public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
        {
            var props = _innerTypeDescriptor.GetProperties(type, container);
            //&& p.GetCustomAttribute<SerialIgnoreAttribute>() == null
            props = props.Where(p => p.Type != typeof(HSDStruct) && p.Name != "trimmedSize" && p.Name != "_s" && p.Name != "length" && p.Name != "stride");
            return props;
        }
    }
}
