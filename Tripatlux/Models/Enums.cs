using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Tripatlux.Models
{
    public enum TypeCar
    {
        [GuidValue("578D6E90-BC57-4DDD-B829-4CD8ABF198CD")]
        [StringValue("Bus")]
        Bus,
        [GuidValue("09E0DE11-DEB7-4D02-895C-7D2B372BB9A7")]
        [StringValue("Break")]
        Break,
        [GuidValue("F2CD47A8-52A3-4AA7-96E2-8512AA2862D4")]
        [StringValue("Voiture de sport")]
        VoitureDeSport,
        [GuidValue("489A5C06-A362-4166-8603-C14C34BBFD2B")]
        [StringValue("Voiture de tourisme")]
        VoitureDeTourisme,
        [GuidValue("74653F4C-A4BB-46E6-8B69-C25FD4872D37")]
        [StringValue("Mini-Bus")]
        MiniBus
    }

    public enum TypeBagage
    {
        [GuidValue("A1C6768D-7C60-4F0F-8B88-DC800EDB3A0F")]
        [StringValue("Aucun bagage")]
        None,

        [GuidValue("53676B81-E138-41B3-B79D-33BEAF6DBE6A")]
        [StringValue("Petit bagage à main")]
        SmallHandBag,

        [GuidValue("E580F187-3020-4030-8EB6-94C3BD9AD9A8")]
        [StringValue("Bagage de cabine")]
        SmallSuiteCase,

        [GuidValue("C79D2EF8-0629-4869-9923-91C2B419ECFA")]
        [StringValue("Valise")]
        SuiteCase,
    }

    public enum TypeStep
    {
        [GuidValue("864C915E-9294-4744-8920-D2633654A27B")]
        [StringValue("Départ")]
        Start,

        [GuidValue("9F1F5AA7-84DE-497F-A059-B97D853FCA5D")]
        [StringValue("Arrivée")]
        Step,

        [GuidValue("D5F9E883-5F49-4FB8-8FC8-0F6B0FA0140F")]
        [StringValue("Etape")]
        End,
    }



    public class StringValue : Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public class GuidValue : Attribute
    {
        private string _value;

        public GuidValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }


    public static class EnumExtensions
    {
        public static string GetString(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
                output = attrs[0].Value;
            return output;
        }

        public static Guid? GetGuid(this Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            GuidValue[] attrs = fi.GetCustomAttributes(typeof(GuidValue), false) as GuidValue[];
            if (attrs.Length > 0)
                output = attrs[0].Value;
            return string.IsNullOrEmpty(output) ? (Guid?)null : new Guid(output);
        }

        public static T GetEnum<T>(this Enum enumType, Guid guid)
        {
            foreach(var item in Enum.GetValues(enumType.GetType()).Cast<Enum>())
            {
                if (item.GetGuid() == guid)
                    return (T)(object)item;
            }
            return default(T);
        }
    }
}