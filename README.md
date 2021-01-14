# Oibi.Repository

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/8130b9924a7f40c38afa2fcf132135cc)](https://www.codacy.com?utm_source=github.com&utm_medium=referral&utm_content=TheTrigger/Oibi.Repository&utm_campaign=Badge_Grade)

Simple definitions for repository pattern

- [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) implemented
- Basic [Context scope](https://mehdi.me/ambient-dbcontext-in-ef6/) (aka ~UnitOfWork)
- `GenericRepository`,`GenericEntityRepository` with and without `PrimaryKey`
- Implements `IQueryable`: `_customerRepository.Where(...)`
- Supports `async`: `_customerRepository.SingleAsync()`
- [By convention, a property named Id or <type name>Id will be configured as the primary key of an entity](https://docs.microsoft.com/it-it/ef/core/modeling/keys#conventions)

## Getting started

## 1. Install Nuget package [Oibi.Repository](https://www.nuget.org/packages/Oibi.Repository/)

```sh
Install-Package Oibi.Repository
```

## 2. Implement `IEntity<TKey>` on your models

Or, you can create your own `BaseEntity`:

```Csharp
public abstract class BaseEntity : IEntity<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}
```

```Csharp
public class Book : BaseEntity
{
    public string Title { get; set; }

    public string Isbn { get; set; }

    public virtual ICollection<BookAuthors> BookAuthors { get; set; }
}
```

## 3. Implement your repository

`GenericEntityRepository<TEntityType>` or `GenericRepository<TEntityType>` abstracts

```CSharp
using Oibi.Repository.Abstracts;

public class CustomerRepository : GenericEntityRepository<Customer>
{
    public CustomerRepository(YourDbContext context) : base(context)
    {
    }
}

```

## 4. Create Database Context Scope

```Csharp
public class YourDbScope : DbContextScope<YourDbContext>
{
    public YourDbScope(YourDbContext context) : base(context)
    {
        YourRepository = yourRepository;
    }

    public YourRepository YourRepository { get; }
}
```

## 5. Register Scope and repositories to DI

All repositories will automatically registered for DI

```Csharp
services.AddDatabaseScope<YourDbContext>();
// ... services.AddDbContext<AutomateContext>(opts => opts.UseSqlServer(_configuration.GetConnectionString("SqlConnection"));
```
