﻿using Microsoft.EntityFrameworkCore;
using OmniDibs.Data;
using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Logic {
    internal class DataIniter {
        private static Random random= new Random();
        public static void InitData() {
            List<Person> persons = PersonManager.GetAllPersons();
            List<Country> countries = CountryManager.GetCountries();
            List<Account> accounts = new();
            foreach (Person person in persons) {
                Country country = countries[random.Next()%countries.Count];
                person.Country = country;
                Account account = PersonGenerator.GetAccount(person);
                person.Accounts.Add(account);
                accounts.Add(account);
            }

            using var db = new OmniDibsContext();
            foreach (var country in countries) {
                db.Add(country);
            }
            foreach (var account in accounts) {
                db.Add(account);
            }
            foreach (var person in persons) {
                db.Add(person);
            }
            Account admin = new Account() {
                Person = persons.Last(),
                Privileges = Privileges.OWNER | Privileges.ADMIN | Privileges.CREATE | Privileges.READ | Privileges.UPDATE | Privileges.DELETE,
                UserName = "BigBoss",
                Password = "Admin"
            };
            db.Add(admin);
            persons.Last().Accounts.Add(admin);

            db.SaveChanges();
        }

        public static void InitFlights() {
            using var db = new OmniDibsContext();
            List<Flight> flights = FlightManager.GetFlights();
            db.AttachRange(flights);
            db.AddRange(flights);
            db.SaveChanges();
        }

        public static void InitPlanes() {
            using var db = new OmniDibsContext();
            List<Airplane> airplanes = AirplaneManager.GetAirplanes();
            db.AddRange(airplanes);
            db.SaveChanges();
        }

        public static void InitCharters() {
            using var db = new OmniDibsContext();
            List<Airplane> airplanes = db.Airplanes.Where(x => true).Include(x => x.Seats).ToList();
            List<Account> accounts = db.Accounts.ToList();
            List<AirplaneBooking> charters = new List<AirplaneBooking>();

            var days = Enumerable.Range(0, DateTime.DaysInMonth(2023, 2)).Select(x => new DateTime(2023, 2, 1 + x));
            foreach(DateTime day in days) {
                if (random.Next(0, 100) < 80) {
                    AirplaneBooking newBooking = new AirplaneBooking {
                        Airplane = airplanes.First(),
                        OrderAccount = accounts[random.Next(0, accounts.Count)],
                        Cost = airplanes.First().Seats.Count * 20000,
                        StartDate = day,
                        EndDate = day
                    };
                    charters.Add(newBooking);
                }
                if (random.Next(0, 100) < 65) {
                    AirplaneBooking newBooking = new AirplaneBooking {
                        Airplane = airplanes.Last(),
                        OrderAccount = accounts[random.Next(0, accounts.Count)],
                        Cost = airplanes.Last().Seats.Count * 5000,
                        StartDate = day,
                        EndDate = day
                    };
                    charters.Add(newBooking);
                }
            }
            foreach(var booking in charters) {
                booking.OrderAccount.Bookings.Add(booking);
            }
            db.AttachRange(accounts);
            db.AddRange(charters);
            db.SaveChanges();
        }
    }
}
