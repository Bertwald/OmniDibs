using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Data {
    internal class AirplaneManager {
        internal static List<Airplane> GetAirplanes() {
            var ret = new List<Airplane>() {
                new Airplane() { Name = "Titanic", Model = "Gulfstream G800" },
                new Airplane() { Name = "Fugitive", Model = "Imagine 200" },
            };
            List<List<Seat>> seatsByPlane = new() {new List<Seat> {
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 1 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 2 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 3 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 4 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 5 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 6 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 7 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 8 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 9 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 10 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 11 },
                                                                        new Seat() {Class = Standard.FIRST_CLASS, IsWindowSeat = true, SeatNumber = 12 }
            } , new List<Seat>(){} };
            for (int seat = 1; seat < 201; seat++) {
                seatsByPlane[1].Add(new Seat {
                    Class = seat < 21 ?  Standard.FIRST_CLASS :
                            seat < 101 ? Standard.BUSINESS :
                                         Standard.ECONOMY,
                    IsWindowSeat = (seat % 8 == 0),
                    SeatNumber = seat
                });
            }
            for (int plane = 0; plane < ret.Count; plane++) {
                var airplane = ret[plane];
                foreach (Seat seat in seatsByPlane[plane]) {
                    airplane.Seats.Add(seat);
                    seat.Airplane = airplane;
                }
            }
            return ret;
        }
    }
}
