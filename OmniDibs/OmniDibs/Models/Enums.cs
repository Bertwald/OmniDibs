
namespace OmniDibs.Models {
    [Flags]
    public enum Privileges : byte {
        NONE        = 0B_0000_0000,
        USER        = 0B_0000_0001,
        ADMIN       = 0B_0000_0010,
        PROVIDER    = 0B_0000_0100,
        OWNER       = 0B_0000_1000,
        CREATE      = 0B_0100_0000,
        READ        = 0B_0001_0000,
        UPDATE      = 0B_0010_0000,
        DELETE      = 0B_1000_0000
    }
    public enum Climate : byte {
        TROPICAL,
        ARID,
        TEMPERATE,
        CONTINENTAL,
        POLAR
    }
    public enum Continent {
        ASIA,
        AFRICA,
        EUROPE,
        NORTH_AMERICA,
        SOUTH_AMERICA,
        OCEANIA,
        ANTARCTICA
    }
    public enum ReturnType : byte {
        CONTINUE = 0,
        SOFTRETURN = 1,
        HARDRETURN = 2
    }
    public enum LoginOptions {
        Login,
        Register
    }
    public enum BookingAlternatives {
        Display_Bookings,
        Remove_a_Booking,
        Make_a_Booking,
        Account_Details,
        Logout
    }
    public enum AdminAlternatives {
        Booking_Statistics,
        Customer_Statistics,
        Business_Configurations,
        Handle_Accounts,
        Logout
    }
    public enum Standard {
        FIRST_CLASS,
        BUSINESS,
        ECONOMY
    }

}
