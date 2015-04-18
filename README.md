NH
==

Пара ссылок на туториалы. 

1. [How to use NHibernate in Visual Studio Projects ](http://www.youtube.com/watch?v=FkmFI736wMU)
2. [C# NHibernate 3.3 Domain, Mapping and NHibernate Helper](C# NHibernate 3.3 Domain, Mapping and NHibernate Helper)
3. [Некоторые виды запросов](http://www.martinwilley.com/net/code/nhibernate/query.html)
4. [Еще сайт с запросами](http://nhibernate.info/blog/2009/12/17/queryover-in-nh-3-0.html)

---

<a name='ogl'>Оглавление</a>
===

1. [LINQ](#linq)
1. [LIKE](#like)
1. [many-to-many](#many-to-many) 


[Оглавление](#ogl)

------------------------------------------------------------------------------------------------------------

###<a name='linq'>LINQ</a>


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
	
[Оглавление](#ogl)

---------------------------------------------------------------------------------------------------------

###<a name='like'>LIKE</a>

Имеется следующий класс

```
public class Payment
	{
		public virtual Int64 ID { get; set; }
		public virtual string Account { get; set; }
		public virtual decimal Sum { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual long IdPayment { get; set; }
		public virtual Guid Gate { get; set; }
		public virtual Guid Provider { get; set; }
	}
```

Есть два хитрых метода для выполнения операцй LIKE как в SQL. В первом случае работае шикарно, можно не использовать MatchMode.Anywhere, но тогда надо расставлять знак %.
Можно еще сделать и так Restrictions.Like("Account", account,  MatchMode.Anywhere). Но такой вид не очень нравится, так как здесь напрямую используем название поля Account. 

А вот во втором варианте нашел единственный способ, который работает. Так как LIKE нужно было выполнить для свойства типа long. Необходимо делать волшебные преобразования Projections.Cast(NHibernateUtil.String, Projections.Property("IdPayment")) и да, здесь тоже используем название поля - не очень красиво. 

```
if (!String.IsNullOrEmpty(account))
	resultList = resultList.And(Restrictions.On<Payment>(p => p.Account).IsLike(account, MatchMode.Anywhere));

if (idPayment != null)
	resultList = resultList.And(Restrictions.Like(
		Projections.Cast(NHibernateUtil.String, Projections.Property("IdPayment")), 
		idPayment.ToString(), 
		MatchMode.Anywhere));
```

[Оглавление](#ogl)

-----------------------------------------------------------------------------------------------

###<a name='many-to-many'>many-to-many</a>

Имеется две табицы company.company и services.services связь у которых реализована как многое ко многим. Связь этихтаблиц происохдит через таблица company.service_list. Нюанс заключается в том что company.company связан с таблицей company.service_list не по своему первичному ключу, а просто по какому-то одному из своих полей. (это добавляет определенной гибкости-можем один список сервисов в последствии использовать для разных компаний, и у нас в таблице company.service_list будет использоваться один и тот же набор данных).

- хотя здесь есть свой недостаток такой гибкости. Если 10 компаний используют один и тот же ключ для списка, и они имеют один и тот же набор сервисов. И тут нам вдруг понадобится у одной компании добавить еще один отдельный сервис. - Проблема.

Enough talking. Let's see an example. 

### Company.company ### table - наши компании которые могут иметь много сервисов

Имя столбца | тип | Описание
---|---|---
**id_company** | guid | Первичный ключ
...|...|...
id_service_list| guid | Гуид для связи со списками в таблице service_list

### Company.service_list ### table - связующая таблица

Имя столбца | тип | Описание
---|---|---
id | guid | Составной ключ - для связи с таблице company по полю id_service_list
service_id| guid | Составной ключ - для связи с таблице services по полю id

### Services.services ### table - наши сервисы которые могут иметь много компаний

Имя столбца | тип | Описание
---|---|---
**id** | guid | Первичный ключ
...|...|...
