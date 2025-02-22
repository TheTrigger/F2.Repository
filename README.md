# F2.Repository

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/437c96b3e26148528379e0da04d97afd)](https://www.codacy.com/gh/TheTrigger/Oibi.Repository/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=TheTrigger/Oibi.Repository&amp;utm_campaign=Badge_Grade)

Simple definitions for repository pattern

- Basic [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) implemented
- Basic [Context scope](https://mehdi.me/ambient-dbcontext-in-ef6/) (aka ~UnitOfWork)
- `GenericRepository`,`GenericEntityRepository` with and without `PrimaryKey`
- Abstracts implement `IQueryable`, example: `_customerRepository.Where(...)`
- `SecureGuidValueGenerator`, `DateTimeValueGenerator`, `ITimestampedEntity`
- [By convention, a property named Id or <type name>Id will be configured as the primary key of an entity](https://docs.microsoft.com/it-it/ef/core/modeling/keys#conventions)

## Getting started

## 1. Install Nuget package [F2.Repository](https://www.nuget.org/packages/Oibi.Repository/)

```sh
Install-Package F2.Repository
```

## 2. Implement `IEntity<TKey>` on your models

Or, you can create your own `BaseEntity`:

```Csharp
public abstract class BaseEntity : IEntity<Guid>, ITimestampedEntity
{
	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
```

```Csharp
public class Book : BaseEntity
{
    public string Title { get; set; }

    public string Isbn { get; set; }

	public virtual ICollection<Author> Authors { get; set; }
}
```

### Create your configuration (see example)

```Csharp
public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.UseAutoGeneratedId();
		builder.UseTimestampedProperty();

		builder.HasMany(s => s.Authors).WithMany(s => s.Books);
	}
}
```

## 3. Implement your repository

`GenericEntityRepository<TEntityType>` or `GenericRepository<TEntityType>` abstracts

```CSharp
using F2.Repository.Abstracts;

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



# Bonus

```
Add-Migration -Context LibraryContext -Project F2.Repository.Demo PublisherOneToMany
Add-Migration -Context LibraryContext -Project F2.Repository.Demo PublisherOneToManyNullable


```