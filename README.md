# Oibi.Repository

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/8130b9924a7f40c38afa2fcf132135cc)](https://www.codacy.com?utm_source=github.com&utm_medium=referral&utm_content=TheTrigger/Oibi.Repository&utm_campaign=Badge_Grade)

Abstract repository pattern that include [AutoMapper](https://github.com/AutoMapper/AutoMapper), supports both entities with `primary key` and without.

BaseRepository implements `IQueryable` so you can **query directly**

```Csharp
_customerRepository.Where(w => w.Name.Contains("wtf"));
```

`abstract GenericRepository<Customer>` <-- No primary key property

`abstract GenericEntityRepository<Customer>` <-- Primary key property `TKey`, usually `Guid`

## TODO

- [ ] events
- [ ] Graphql demo

## Getting started

## 1. Install Nuget package [Oibi.Repository](https://www.nuget.org/packages/Oibi.Repository/)

```sh
Install-Package Oibi.Repository
```

## 2. Implement `IEntity<TKey>` on your models

Optionally, you can create your own `BaseEntity`:

```CSharp
public abstract class BaseEntity : IEntity<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = CreateCryptographicallySecureGuid();

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    protected static Guid CreateCryptographicallySecureGuid()
    {
        using var provider = new RNGCryptoServiceProvider();

        var bytes = new byte[16];
        provider.GetBytes(bytes);

        return new Guid(bytes);
    }
}
```

```CSharp
public class Book : BaseEntity
{
    public string Title { get; set; }

    public string Isbn { get; set; }

    public virtual ICollection<BookAuthors> BookAuthors { get; set; }
}
```

## 3. Implement your repository from `GenericEntityRepository<TEntityType>` or `GenericRepository<TEntityType>` abstract

```CSharp
using AutoMapper;
using Oibi.Repository.Abstracts;

public class CustomerRepository : GenericEntityRepository<Customer>
{
    public CustomerRepository(WorkifyContext context, IMapper mapper) : base(context, mapper)
    {
    }

    // ... continue with your methods
}
```

- [By convention, a property named Id or <type name>Id will be configured as the primary key of an entity.](https://docs.microsoft.com/it-it/ef/core/modeling/keys#conventions)
  Here I needed to choose a single convention, so I used Id as a standard `primary key` property.

## 4. Configure your mappers

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

## 5. Add your repository to services

Startup.cs ConfigureServices():

```Csharp
// ...
services.AddAutoMapper(typeof(MappingProfile)); // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection
services.AddScoped<CustomerRepository>();

// CustomerService left out for brevity
// ...
```

## You're ready

See Demo project

## Usage example

```Csharp
/// <summary>
/// Create new customer
/// </summary>
[HttpPost]
public async Task<IActionResult> Create(CreateCustomerDto dto)
{
    // 1. CreateCustomerDto dto is auto-mapped to TEntity object
    // 2. entity is created
    // 3. EF entity is mapped to CustomerResponseDto and returned
    // nb; with EFCore 3+ the primary key Id is the effective id after context save
    var mapped = _customerRepository.Create<CustomerResponseDto>(dto);

    await _customerRepository.SaveAsync(); // ..

    return Ok(mapped);
}
```

## Snippets

```Csharp
_customerRepository.Retrieve(id);
_customerRepository.Retrieve<CustomerDto>(id);

_customerRepository.Delete(id);
_customerRepository.Delete(entity);
_customerRepository.Update(id, dto);

_customerRepository.Update<CustomerDto>(id, entity);
_customerRepository.Update<CustomerDto>(id, dto);

...etc
```
