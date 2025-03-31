using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Waiters
{
    public sealed class Waiter : AggregateRoot
    {
        private Waiter() { }

        public Waiter(WaiterId id, string fullName, string dni, PhoneNumber phoneNumber, DateOnly hireDate)
        {
            Id = id;
            FullName = fullName;
            DNI = dni;
            PhoneNumber = phoneNumber;
            HireDate = hireDate;
        }

        public WaiterId Id { get; private set; } = null!;
        public string FullName { get; private set; } = null!;
        public string DNI { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public DateOnly HireDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        public static Waiter UpdateWaiter(WaiterId id, string fullName, string dni, PhoneNumber phoneNumber, DateOnly hireDate)
        {
            return new Waiter
            (
                id,
                fullName,
                dni,
                phoneNumber,
                hireDate
            );
        }
    }
}
