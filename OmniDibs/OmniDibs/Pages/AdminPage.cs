using OmniDibs.Interfaces;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;

namespace OmniDibs.Pages {
    internal class AdminPage : DefaultMenu<AdminAlternatives>, IRunnable {
        private Account _account;
        internal AdminPage(Account account) : base("AdminPage") { _account = account; }

        public ReturnType Run() {
            while (true) {
                GUI.ClearWindow();
                GUI.printWindow($"| {_title} |", 0, 0, 100, 20);
                return RunMenu();
            }
        }

        protected override ReturnType ExecuteMappedAction(AdminAlternatives e) => e switch {
            AdminAlternatives.Booking_Statistics => ShowBookingStatistics(),
            AdminAlternatives.Customer_Statistics => ShowCustomerStatistics(),
            AdminAlternatives.Business_Configurations => ConfigureBusinessObjects(),
            AdminAlternatives.Handle_Accounts => HandleAccounts(),
            AdminAlternatives.Logout => ReturnType.HARDRETURN,
            _ => ReturnType.CONTINUE
        };

        private ReturnType HandleAccounts() {
            throw new NotImplementedException();
        }

        private ReturnType ConfigureBusinessObjects() {
            throw new NotImplementedException();
        }

        private ReturnType ShowCustomerStatistics() {
            throw new NotImplementedException();
        }

        private ReturnType ShowBookingStatistics() {
            throw new NotImplementedException();
        }

        protected override AdminAlternatives GetE(int i) {
            return (AdminAlternatives)i;
        }
    }
}
