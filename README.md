[![Codacy Badge](https://api.codacy.com/project/badge/Grade/8130b9924a7f40c38afa2fcf132135cc)](https://www.codacy.com?utm_source=github.com&utm_medium=referral&utm_content=TheTrigger/Oibi.Repository&utm_campaign=Badge_Grade)

# Oibi.Repository

Abstraction of repository pattern that include [AutoMapper](https://github.com/AutoMapper/AutoMapper).

There are **two types of a generic repository**, one that does not implement operations by `primary key`, and the other that implements operations by `primary key` (such as retrieve/update/delete).

`GenericRepository<Customer>` <-- No primary key property

`GenericEntityRepository<Customer>` <-- Primary key property `Guid`

You could also create your own abstract:

```Csharp
public abstract class MyOwnGenericEntityRepository<T> : RepositoryEntityBase<T, <YOUR_PRIMARY_KEY_TYPE_HERE> where T : class, IEntity<YOUR_PRIMARY_KEY_TYPE_HERE>, new()
{
    protected MyOwnGenericEntityRepository(DbContext repositoryContext, IMapper mapper);
}
```

_Unfortunately_, your classes that have a primary key, should implement the interface `IEntity<PKTYPE>` class.

[As aspnetcore convention](https://docs.microsoft.com/it-it/ef/core/modeling/keys#conventions), the primary key would be named `Id`, not `IdGandalf` of `Customer.CustomerIdCustomerWtfCustomer`. Yes, `CustomerId` would be legal. But not here for a obvious reason.

Example:

```Csharp
public class Customer : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; }
}
```

# Getting started

```Sh
Install-Package Oibi.Repository
```

## AutoMapper example configuration

```Csharp
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Implicit time to long
        CreateMap<long, TimeSpan>().ConstructUsing(x => TimeSpan.FromTicks(x));
        CreateMap<TimeSpan, long>().ConstructUsing(x => x.Ticks);

        // Customer
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        // Subscription
        CreateMap<CreateSubscriptionDto, Subscription>();
        CreateMap<UpdateSubscriptionDto, Subscription>();

        // Employee
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
    }
}
```

## Create your own Repository classes

```Csharp
using AutoMapper;
using Oibi.Repository.Abstracts;

public class CustomerRepository : GenericEntityRepository<Customer>
{
    public CustomerRepository(WorkifyContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
    {
    }
}

public class EmployeeRepository : GenericEntityRepository<Employee>
{
    public EmployeeRepository(WorkifyContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
    {
    }
}

public class SubscriptionRepository : GenericEntityRepository<Subscription>
{
    public SubscriptionRepository(WorkifyContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
    {
    }
}

public class TicketRepository : GenericEntityRepository<Ticket>
{
    public TicketRepository(WorkifyContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
    {
    }
}
```

### Create your own Repository Wrapper

```Csharp
public interface IRepositoryWrapper
{
    CustomerRepository Customers { get; }
    EmployeeRepository Employees { get; }
    SubscriptionRepository Subscriptions { get; }
    TicketRepository Tickets { get; }

    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}
```

```Csharp
public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly WorkifyContext _context;
    private readonly IMapper _mapper;

    private CustomerRepository _customers;
    private EmployeeRepository _employees;
    private SubscriptionRepository _subscriptions;
    private TicketRepository _tickets;

    public RepositoryWrapper(WorkifyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // C# 8 syntax
    public CustomerRepository Customers => _customers ??= new CustomerRepository(_context, _mapper);

    public EmployeeRepository Employees => _employees ??= new EmployeeRepository(_context, _mapper);

    public SubscriptionRepository Subscriptions => _subscriptions ??= new SubscriptionRepository(_context, _mapper);

    public TicketRepository Tickets => _tickets ??= new TicketRepository(_context, _mapper);

    public Task<int> SaveAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
}
```

## Register repository service

### Startup.cs ConfigureServices

```Csharp
services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
```

# Usage example

```Csharp
/// <summary>
/// Create new customer
/// </summary>
[HttpPost]
public async Task<IActionResult> Create(CreateCustomerDto dto)
{
    // new customer will be created and mapped to CustomerResponseDto
    var mapped = _repositories.Customers.Create<CustomerResponseDto>(dto);

    await _repositories.SaveAsync();

    return Ok(mapped);
}
```

## Snippets

```Csharp
_repositories.Customers.Retrieve(id);
_repositories.Customers.Retrieve<CustomerDto>(id);

_repositories.Customers.Delete(id);
_repositories.Customers.Delete(entity);

_repositories.Customers.Update(id, dto);
_repositories.Customers.Update<CustomerDto>(id, entity);
_repositories.Customers.Update<CustomerDto>(id, dto);
...etc
```

The repository also implementing `IQueryable` so you could directly use `Linq` on property:

```Csharp
_repositories.Customers.Where(w => w.Name.Contains("wtf"));
```
