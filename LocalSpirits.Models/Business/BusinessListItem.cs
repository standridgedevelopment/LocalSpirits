using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Business
{
    public class BusinessListItem
    {
        public int ID { get; set; }
        [DisplayName("Business Type")]
        public string TypeOfEstablishment { get; set; }
        [DisplayName("Business Name")]
        public string Name { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public int? CityID { get; set; }
        public string State { get; set; }
        public string FullState { get 
            {
                return GetFullStateName(State);
            }
            set { }
        }
        [DisplayName("Zip Code")]
        public int? ZipCode { get; set; }
        public ICollection<LocalSpirits.Data.Event> Events { get; set; }

        public int RateCount
        {
            get
            {
                return Ratings.Count;
            }

        }
        public decimal RateAverage
        {

            get

            {
                return Math.Round(Ratings.Sum(m => (decimal)m.Rating) / (decimal)RateCount, 1);

            }

        }
        public ICollection<Data.Visited> Ratings { get; set; }

        public string GetFullStateName(string stateName)
        {
            if (stateName.ToLower() == "al") return "Alabama";
            if (stateName.ToLower() == "ak") return "Alaska";
            if (stateName.ToLower() == "az") return "Arizona";
            if (stateName.ToLower() == "ar") return "Arkansas";
            if (stateName.ToLower() == "ca") return "California";
            if (stateName.ToLower() == "co") return "Colorado";
            if (stateName.ToLower() == "ct") return "Connecticut";
            if (stateName.ToLower() == "de") return "Delaware";
            if (stateName.ToLower() == "dc") return "Washington D.C";
            if (stateName.ToLower() == "fl") return "Florida";
            if (stateName.ToLower() == "ga") return "Georgia";
            if (stateName.ToLower() == "hi") return "Hawaii";
            if (stateName.ToLower() == "id") return "Idaho";
            if (stateName.ToLower() == "il") return "Illinois";
            if (stateName.ToLower() == "in") return "Indiana";
            if (stateName.ToLower() == "ia") return "Iowa";
            if (stateName.ToLower() == "ks") return "Kansas";
            if (stateName.ToLower() == "ky") return "Kentucky";
            if (stateName.ToLower() == "la") return "Louisiana";
            if (stateName.ToLower() == "me") return "Maine";
            if (stateName.ToLower() == "md") return "Maryland";
            if (stateName.ToLower() == "ma") return "Massachusetts";
            if (stateName.ToLower() == "mi") return "Michigan";
            if (stateName.ToLower() == "mn") return "Minnesota";
            if (stateName.ToLower() == "ms") return "Mississippi";
            if (stateName.ToLower() == "mo") return "Missouri";
            if (stateName.ToLower() == "mt") return "Montana";
            if (stateName.ToLower() == "ne") return "Nebraska";
            if (stateName.ToLower() == "nv") return "Nevada";
            if (stateName.ToLower() == "nh") return "New Hampshire";
            if (stateName.ToLower() == "nj") return "New Jersey";
            if (stateName.ToLower() == "nm") return "New Mexico";
            if (stateName.ToLower() == "ny") return "New York";
            if (stateName.ToLower() == "nc") return "North Carolina";
            if (stateName.ToLower() == "nd") return "North Dakota";
            if (stateName.ToLower() == "oh") return "Ohio";
            if (stateName.ToLower() == "ok") return "Oklahoma";
            if (stateName.ToLower() == "or") return "Oregon";
            if (stateName.ToLower() == "pa") return "Pennsylvania";
            if (stateName.ToLower() == "ri") return "Rhode Island";
            if (stateName.ToLower() == "sc") return "South Carolina";
            if (stateName.ToLower() == "sd") return "South Dakota";
            if (stateName.ToLower() == "tn") return "Tennessee";
            if (stateName.ToLower() == "tx") return "Texas";
            if (stateName.ToLower() == "ut") return "Utah";
            if (stateName.ToLower() == "vt") return "Vermont";
            if (stateName.ToLower() == "va") return "Virgina";
            if (stateName.ToLower() == "wa") return "Washington";
            if (stateName.ToLower() == "wv") return "West Virginia";
            if (stateName.ToLower() == "wi") return "Wisconsin";
            if (stateName.ToLower() == "wy") return "Wyoming";
            return null;
        }
    }
}
