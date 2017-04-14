using Newtonsoft.Json;
using Sefacan.Core.Enums;
using Sefacan.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sefacan.Core.Entities
{
    public class Setting : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Value { get; set; }

        [MaxLength(500)]
        public string SelectedValue { get; set; }

        public SettingType Type { get; set; }
        public Input InputType { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool HasValue
        {
            get
            {
                return !string.IsNullOrEmpty(Value);
            }
        }

        [NotMapped]
        [JsonIgnore]
        public int IntValue
        {
            get
            {
                return Value.ToInt();
            }
        }

        [NotMapped]
        [JsonIgnore]
        public bool BoolValue
        {
            get
            {
                return Value.ToBoolean();
            }
        }

        [NotMapped]
        [JsonIgnore]
        public decimal DecimalValue
        {
            get
            {
                return Value.ToDecimal();
            }
        }

        [NotMapped]
        [JsonIgnore]
        public DateTime DateTimeValue
        {
            get
            {
                return Value.ToDateTime();
            }
        }

        [NotMapped]
        [JsonIgnore]
        public float FloatValue
        {
            get
            {
                return Value.ToFloat();
            }
        }

        [NotMapped]
        [JsonIgnore]
        public double DoubleValue
        {
            get
            {
                return Value.ToDouble();
            }
        }
    }
}