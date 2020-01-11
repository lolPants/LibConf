using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace LibConf.Yaml
{
    internal sealed class FloatTypeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type.IsAssignableFrom(typeof(float));
        }

        public object ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var f = (float)value;
            emitter.Emit(new Scalar(f.ToString("R")));
        }
    }
}
