﻿using OmniDibs.Interfaces;
using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Pages {
    internal class BookingPage : IRunnable {
        private Account _account;
        internal BookingPage(Account account) { _account = account; }

        internal ReturnType Redirect() {
            throw new NotImplementedException();
        }

        public ReturnType Run() {
            throw new NotImplementedException();
        }
    }
}