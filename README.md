NH
==

Пара ссылок на туториалы. 

1. [How to use NHibernate in Visual Studio Projects ](http://www.youtube.com/watch?v=FkmFI736wMU)
2. [C# NHibernate 3.3 Domain, Mapping and NHibernate Helper](C# NHibernate 3.3 Domain, Mapping and NHibernate Helper)


Additional Restrictions

Some SQL operators/functions do not have a direct equivalent in C#. (e.g., the SQL where name like '%anna%'). These operators have overloads for QueryOver in the Restrictions class, so you can write:

        .Where(Restrictions.On<Cat>(c => c.Name).IsLike("%anna%"))
        
        

для того чтобы выбрать нужные поля

IList selection =
    session.QueryOver<Cat>()
        .Select(
            c => c.Name,
            c => c.Age)
        .List<object[]>();
        
        var catDetails =
    session.QueryOver<Cat>()
        .Select(
            c => c.Name,
            c => c.Age)
        .List<object[]>()
        .Select(properties => new {
            CatName = (string)properties[0],
            CatAge = (int)properties[1],
            });
