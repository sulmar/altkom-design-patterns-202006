using System.Threading;
using System.Threading.Tasks;
using MediatorPattern.IServices;
using MediatR;

namespace MediatorPattern.Models
{
    public class AddCustomerEvent : INotification // mark interface
    {
        public AddCustomerEvent(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; private set; }
    }

    public class SaveCustomerHandler : INotificationHandler<AddCustomerEvent>
    {
        private readonly IServices.ICustomerRepository customerRepository;

        public SaveCustomerHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Task Handle(AddCustomerEvent notification, CancellationToken cancellationToken)
        {
            customerRepository.Add(notification.Customer);

            return Task.CompletedTask;
        }
    }

    public class SendMessageHandler : INotificationHandler<AddCustomerEvent>
    {
        public Task Handle(AddCustomerEvent notification, CancellationToken cancellationToken)
        {
            Customer customer = notification.Customer;

            System.Console.WriteLine($"Welcome {customer.FullName}");

            return Task.CompletedTask;
        }
    }

    public class GetCustomerRequest : IRequest<Customer>
    {
        public GetCustomerRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }        
    }

    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, Customer>
    {
        private readonly IServices.ICustomerRepository customerRepository;

        public GetCustomerRequestHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Task<Customer> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer customer = customerRepository.Get(request.Id);

            return Task.FromResult(customer);
        }
    }
}
