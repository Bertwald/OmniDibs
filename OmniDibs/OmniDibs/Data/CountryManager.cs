using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Data {
    internal class CountryManager {

        internal static List<Country> GetCountries() {
            var ret = new List<Country>() {
                new Country() { CountryName = "England", Languages = { "English" }, ContinentsString = "EUROPE", Climate = Climate.TEMPERATE, BigMacIndex = 2.68F },
                new Country() { CountryName = "Turkey", Languages = { "Turkish", "English" }, ContinentsString = "EUROPE,ASIA", Climate = Climate.TEMPERATE, BigMacIndex = 2.68F },
                new Country() { CountryName = "Sweden", Languages = { "Swedish", "English" }, ContinentsString = "EUROPE", Climate = Climate.CONTINENTAL, BigMacIndex = 5.59F },
                new Country() { CountryName = "Canada", Languages = { "French", "English" }, ContinentsString = "NORTH_AMERICA", Climate = Climate.CONTINENTAL, BigMacIndex = 5.25F },
                new Country() { CountryName = "France", Languages = { "French" }, ContinentsString = "EUROPE,NORTH_AMERICA,SOUTH_AMERICA,AFRICA,OCEANIA", Climate = Climate.TEMPERATE, BigMacIndex = 5.14F },
                new Country() { CountryName = "Antarctica", Languages = { "English" }, ContinentsString = "ANTARCTICA", Climate = Climate.POLAR, BigMacIndex = float.MaxValue },

            };
            return ret;
        }
        //Country country = new Country() { CountryName = "Turkey", Languages = { "Turkish", "English" }, ContinentsString = "EUROPE,ASIA", Climate = Climate.TEMPERATE, BigMacIndex = 2.68F };
    }
}
