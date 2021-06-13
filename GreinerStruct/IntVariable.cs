using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct
{
    internal class IntVariable
    {
        public string Value { get; private set; }

        public IntVariable(string value)
        {
            Value = value;
        }

        public IntVariable(int value)
        {
            Value = value.ToString();
        }

        public IntVariable Add(int num)
        {
            if (int.TryParse(Value, out var numValue))
            {
                Value = (numValue + num).ToString();
            }
            else
            {
                Value += $" + {num}";
            }
            return this;
        }

        public IntVariable Subtract(int num)
        {
            if (int.TryParse(Value, out var numValue))
            {
                Value = (numValue - num).ToString();
            }
            else
            {
                Value += $" - {num}";
            }
            return this;
        }

        public IntVariable ToNegative()
        {
            if (int.TryParse(Value, out var numValue))
            {
                Value = (-numValue).ToString();
            }
            else
            {
                Value = $"-{Value}";
            }

            return this;
        }

        public override string ToString() => Value;
    }
}
