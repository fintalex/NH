NH
==

Пара ссылок на туториалы. 

1. [How to use NHibernate in Visual Studio Projects ](http://www.youtube.com/watch?v=FkmFI736wMU)
2. [C# NHibernate 3.3 Domain, Mapping and NHibernate Helper](C# NHibernate 3.3 Domain, Mapping and NHibernate Helper)
3. [Некоторые виды запросов](http://www.martinwilley.com/net/code/nhibernate/query.html)
4. [Еще сайт с запросами](http://nhibernate.info/blog/2009/12/17/queryover-in-nh-3-0.html)


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
            
LINQ 
===

```
from ugl in Usergridlayouts
    join sl in Sharedlayouts
        on ugl.ID equals sl.LayoutID into joined
	from res in joined.DefaultIfEmpty()
    where ugl.Page == "OrderListLayout" && (ugl.UserID == 11427 || res.UserID == 11427)
    select new
	{
		ugl.ID, ugl.TextLayout, ugl.UserID, ugl.Page, ugl.TextLayoutXML, ugl.XMLformat,
		Name = (res.UserID == 11427) ? ugl.Name+" (Admin)" : ugl.Name, 
		Selected = (res.UserID == 11427)? res.Selected : ugl.Selected 
	}
	```
	
	
