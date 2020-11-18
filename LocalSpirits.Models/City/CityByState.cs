using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.City
{
    public class CityByState
    {
        public StateName? StateResult { get; set; }
        public string State { get; set; }


        public StateName? AbreviateState(string stateName)
        {
            if (stateName.ToLower() == "alabama" || stateName.ToLower() == "al") return StateName.AL;
            if (stateName.ToLower() == "alaska" || stateName.ToLower() == "ak") return StateName.AK;
            if (stateName.ToLower() == "arizona" || stateName.ToLower() == "az") return StateName.AZ;
            if (stateName.ToLower() == "arkansas" || stateName.ToLower() == "ar") return StateName.AR;
            if (stateName.ToLower() == "california" || stateName.ToLower() == "ca") return StateName.CA;
            if (stateName.ToLower() == "colorado" || stateName.ToLower() == "co") return StateName.CO;
            if (stateName.ToLower() == "conneticut" || stateName.ToLower() == "ct") return StateName.CT;
            if (stateName.ToLower() == "delaware" || stateName.ToLower() == "de") return StateName.DE;
            if (stateName.ToLower().Contains("dc") || stateName.ToLower().Contains("d.c")) return StateName.DC;
            if (stateName.ToLower() == "florida" || stateName.ToLower() == "fl") return StateName.FL;
            if (stateName.ToLower() == "georigia" || stateName.ToLower() == "ga") return StateName.GA;
            if (stateName.ToLower() == "hawaii" || stateName.ToLower() == "hi") return StateName.HI;
            if (stateName.ToLower() == "idaho" || stateName.ToLower() == "id") return StateName.ID;
            if (stateName.ToLower() == "illinois" || stateName.ToLower() == "il") return StateName.IL;
            if (stateName.ToLower() == "indiana" || stateName.ToLower() == "in") return StateName.IN;
            if (stateName.ToLower() == "iowa" || stateName.ToLower() == "ia") return StateName.IA;
            if (stateName.ToLower() == "kansas" || stateName.ToLower() == "ks") return StateName.KS;
            if (stateName.ToLower() == "kentucky" || stateName.ToLower() == "ky") return StateName.KY;
            if (stateName.ToLower() == "louisiana" || stateName.ToLower() == "la") return StateName.LA;
            if (stateName.ToLower() == "maine" || stateName.ToLower() == "me") return StateName.ME;
            if (stateName.ToLower() == "maryland" || stateName.ToLower() == "md") return StateName.MD;
            if (stateName.ToLower() == "massachusetts" || stateName.ToLower() == "ma") return StateName.MA;
            if (stateName.ToLower() == "michigan" || stateName.ToLower() == "mi") return StateName.MI;
            if (stateName.ToLower() == "minnesota" || stateName.ToLower() == "mn") return StateName.MN;
            if (stateName.ToLower() == "mississippi" || stateName.ToLower() == "ms") return StateName.MS;
            if (stateName.ToLower() == "missouri" || stateName.ToLower() == "mo") return StateName.MO;
            if (stateName.ToLower() == "montana" || stateName.ToLower() == "mt") return StateName.MT;
            if (stateName.ToLower() == "nebraska" || stateName.ToLower() == "ne") return StateName.NE;
            if (stateName.ToLower() == "nevada" || stateName.ToLower() == "nv") return StateName.NV;
            if (stateName.ToLower() == "new hampshire" || stateName.ToLower() == "nh") return StateName.NH;
            if (stateName.ToLower() == "new jersey" || stateName.ToLower() == "nj") return StateName.NJ;
            if (stateName.ToLower() == "new mexico" || stateName.ToLower() == "nm") return StateName.NM;
            if (stateName.ToLower() == "new york" || stateName.ToLower() == "ny") return StateName.NY;
            if (stateName.ToLower() == "north carolina" || stateName.ToLower() == "nc") return StateName.NC;
            if (stateName.ToLower() == "north dakota" || stateName.ToLower() == "nd") return StateName.ND;
            if (stateName.ToLower() == "ohio" || stateName.ToLower() == "oh") return StateName.OH;
            if (stateName.ToLower() == "oklahoma" || stateName.ToLower() == "ok") return StateName.OK;
            if (stateName.ToLower() == "oregon" || stateName.ToLower() == "in") return StateName.IN;
            if (stateName.ToLower() == "pennsylvania" || stateName.ToLower() == "pa") return StateName.PA;
            if (stateName.ToLower() == "rhode island" || stateName.ToLower() == "ri") return StateName.RI;
            if (stateName.ToLower() == "south carolina" || stateName.ToLower() == "sc") return StateName.SC;
            if (stateName.ToLower() == "south dakota" || stateName.ToLower() == "sd") return StateName.SD;
            if (stateName.ToLower() == "tennessee" || stateName.ToLower() == "tn") return StateName.TN;
            if (stateName.ToLower() == "texas" || stateName.ToLower() == "tx") return StateName.TX;
            if (stateName.ToLower() == "utah" || stateName.ToLower() == "ut") return StateName.UT;
            if (stateName.ToLower() == "vermont" || stateName.ToLower() == "vt") return StateName.VT;
            if (stateName.ToLower() == "virginia" || stateName.ToLower() == "va") return StateName.VA;
            if (stateName.ToLower() == "washington" || stateName.ToLower() == "wa") return StateName.WA;
            if (stateName.ToLower() == "west virginia" || stateName.ToLower() == "wv") return StateName.WV;
            if (stateName.ToLower() == "wisconsin" || stateName.ToLower() == "wi") return StateName.WI;
            if (stateName.ToLower() == "wyoming" || stateName.ToLower() == "wy") return StateName.WY;
            return null;
        }
    }
}
