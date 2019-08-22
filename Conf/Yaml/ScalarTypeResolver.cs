using System;
using System.Text.RegularExpressions;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace LibConf.Yaml
{
    internal sealed class ScalarTypeResolver : INodeTypeResolver
    {
        public bool Resolve(NodeEvent nodeEvent, ref Type currentType)
        {
            if (currentType == typeof(object))
            {
                if (nodeEvent is Scalar scalar)
                {
                    if (Regex.IsMatch(scalar.Value, @"^(true|false)$", RegexOptions.IgnorePatternWhitespace))
                    {
                        currentType = typeof(bool);
                        return true;
                    }

                    if (Regex.IsMatch(scalar.Value, @"^-? ( 0 | [1-9] [0-9]* )$", RegexOptions.IgnorePatternWhitespace))
                    {
                        currentType = typeof(int);
                        return true;
                    }

                    if (Regex.IsMatch(scalar.Value, @"^-? ( 0 | [1-9] [0-9]* ) ( \. [0-9]* )? ( [eE] [-+]? [0-9]+ )?$", RegexOptions.IgnorePatternWhitespace))
                    {
                        currentType = typeof(float);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
