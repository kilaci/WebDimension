# WebDimension
Web oldal Dimension ERP rendszerhez

## Alapkövetelmények
- .net core - Technológia melyet általános körben elterjedt programozói körökben, C# nyelven programozunk a legtöbbet használt össze platformon. (Window, Linux, Mac OS, Mobil….) 
- Tovább fejleszthető maradjon.

## Vázlat
- elsődleges cél: web alkalmazás készítése (MVC)
- továbbfejlesztés: adatok szolgáltatás és az üzleti logika elérése webAPI-n keresztül (MVC)
- továbbfejlesztés: desktop alkalmazás készítése a webAPIra alapozva (WPF)
- mobil alkalmazás készítése a webAPIra alapozva (Xamarin)
- továbbfejlesztés: SPA (Single Page Application) készítése a webAPIra alapozva (Blazor)

## Webalkalmazás
Egy továbbfejleszthető információs alkalmazást hozok létre, ahol
 - Rendszergazdák jogosultásgokat tudnak kiosztani, funkciókat felhasználóhoz rendelni.
 - Vezetők tudnak céges informcáiókhoz jutni, feladatot kiosztani. 
 - Alkalmazottak számukra fontos információkat megtekinteni

 
## Architektúra 1 gondolatra
Az egész alkalmazás kiemelt fontosságú az architektúra felépítése szempontjából. Szeretnék egy könnyen menedzselhető kódot írni, jól elkülönített részekkel. Az alkalmazásnak mindenképpen perzisztens adattárolásra lesz szüksége, ezért adatbázis használata elengedhetetlen.
Felhasználói felületből, felhasználói alkalmazás oldalról kiindulva egyszerűnek tünhet az MVC Modelt használni. Ha Modellként az Entity Framework-t használom, melyből még az adatbázist le tudnám generálni, majdhogynem el sem különülne az adabtázisomtól és már csak a View-t Controller-t kellene megírnom, létrehoznom. 

DB <----> MVC WebAPP (EF Model, View, Controller)

## Architektúra 2 gondolatra
Mivel továbbfejleszthető platformot szeretnék építeni melynek alapkőveként gondolok arra, hogy WebAPI-t kell fejlesztenem, a fenti struktúrába nehezen tudom elképzelni, hogy hova és építeném. A Modellnek a WebApi-ból kell dolgoznia, de akkor az üzleti logikát vajon hova építem be.

Elgondolásom szerint ha az EF Modelt kiemelem az MVC-ből, és azt úgyis az adatbázissal majdnem egyező elemként kezelem, akkor az EF model már lehet egy adatokat "szolgáltató" rész MVC WebApp felé. Ebben az esetben az MVC belül a Modellünk már egy valós megjelenítési modell lehet, melyhez az adatokat az EF Modell szolgáltatja. Ezen megvalósítás esetben a WebApi kapcsolódhat az EF Modelhez. Felmerül, hogy az üzleti logika ebben az esetben, ha az MVC MOdel Controllerjében, View-n belül valósúl meg, akkor az mennyinre lesz használható továbbfejlesztés esetében. A legtöbb esetben az EF model üzleti logikáját meg kellene ismételnem a Controller és a View-ban.

DB <----> EF MOdel <----> MVC WebApp (View Model, Controller, View)

## Végső architektúra
Mindenképp szeretném úgy kialakítani az architektúrát, hogy az adatok összes manipulációja, lekérdezése egy helyen, egy "dobozba" kerüljön. Nevezzük ezt Repositorynak a továbbiekban. További fontos kérdés, hogy az üzelti logikát hova helyezzem el. A további fejlesztés érdekében, ezt is egy külön "dobozba" teszem mely felelős a már nem nyers, de az applikáció érdekében összehangoltan müködő lekérdezésekért, melyek bedobozolva tartalmzzák az üzleti logikát. Ezt a továbbiakban Service-nek fogom nevezni. Felépítésemmel, a fent említett jól elkülönült feladatok megfogalmazásával egy moduláris felépítésű alkalmazást tudok létrehozni. Tehát a jól elkülönült részeket akár le is cserélhetjük, újrafejleszthetjük. 
A WebAPI a Servicenél tud becsatlakozni a folyamatokba, így bármilyen alkalamazás építhető majd köré. Ez fontos szempont volt a továbbfejleszthetőség fényében.

DB <----> Repository (EF MOdel) <----> Service <----> MVC WebApp (View Model, Controller, View)

Persze ez a megoldás mindenképp költséges, az erőforrás még hagyján, de ez esetben sem az idő sem az élettartam sem az, hogy szakdolgozatként készül, nem elhanyagolható tényező. 

## Objektumorientált tervezési elképzelések. 
 - Gyenge csatolás (Low Coupling) - Szeretném kizárni, hogy ha egyik elemen változtatok, akkor ne legyen változási kényszerem más elemekkel kapcsolatosan. A változás továbbterjedését szeretném elkerülni. A gyenge csatolást célként tüzőm ki, mert könnyebb újrafelhasználni.
 - Erős kohézió (High cohesion) - A "dobozok" felelősségi kapcsolata egymáshoz viszonyítva ne legyen túl sok egymástól független felelősség! Cél az adott elemek erős függése és koncentárciója. 
 - Költség - hosszú élettartamra és továbbfejleszthetőséget mindvégig figyelemben kell tartanom. A költség az építőelemek számának nővelésével jár. Az indirekciót, áttételeket ésszel kell kezelni elsősorban az élettartam függvényében. Ez egy befektetés a jövőbe.

## Feladatok, felelősségi körök

### DB
- Entity Framework Core Project választás a rengeteg Provider miatt jó választásnak tűnik. Az adatbázis tervezést és telepítést is az EF segítségével valósítom meg.
- docker használata - most nem tervezem a Linuxra való fejleszthetőséget, de a későbbiekben ez se legyen akadály, ezért docker containerbe szeretném zárni az alkalmazásunkat és függőségeit. 
- MS SQL - A docker containernek van linux támogatása, ezért nem kérdés, hogy a Dimension által is használt MS SQL használjuk adatbázis motorként.
- sqlight - A fejleszthetőség erőforrása és egyszerűsége miatt a kezdetekben sqllight használatát tervezem, mely a későbbiekben egy az egyben beintegrálható ms sql-re

Van-e kockázatom EF használatával? Az adatok mennyiségéből kiindulva teljesítmény problémára nem számolok. Az, hogy csak relációs adatbázis kezelést valósíthatok meg, szintén nem tartom kockázati tényezőnek. 
Műszaki adósság: https://netacademia.blog.hu/2016/06/21/a_muszaki_adossag_fogalma

### Repository
- CRUD műveletek elvégzése
- Listázási feladatok (szűrés, sorbarendezés, lapozás)
- Közvetlen adatbázist ne tudjak elérni, csak "offline" adatokkal tudjak dolgozni. (IQueryable példány ne adjon vissza!)
- Adat modelleket fogok gyártani (Külön kimeneti osztályokat nem fogok használni, LINQ)

### Service
- A repository adatmodell és az alkalmazás viewmodell között átalakítást kell elvégezni. (Data mapping)
- Validálás - a bonyolultabb validálások itt történnek.

### Web UI
- http kérés fogadása és válasz küldése
  MVC keretrendszer
- Felhasználó azonosítás (authentication)
  ASP.NET Core Indentity
- Jogosultság kezelés (authorization)
  ASP.NET Core Indentity
- Bejövő adatok validálása
  MVC ViewModel

Az ASP.NET Core Indentity nagyon jó beépíthető elem, a kockázati tényező abban rejlik, hogy vigyáznom kell továbbfejlesztés korlátozására. Ha majd több alkalmazásom lesz akkor szükség lehet elosztott bejelentkezés és jogosultságkezelésre. Ezért a Webapi fejlesztésénél bevezetem az Identity server használatát, mely reményeim szerint leveszi vállamról ezt a gondot.


## és még
- docker container fejlesztési környezet
- visual studio code
 - Extensions - C#, C# Extensions, C# XML Documentation Comments


 ## Kódstruktúra
 Projektek:
 - webdimension.Data   (dotnet new classlib)-template
  Névterek:
  - webdimension.Data
  - webdimension.Repository
  NuGet:
  - dotnet add package Microsoft.EntityFrameworkCore --version 3.1.8

 - webdimension.WebUI  (dotnet new mvc)-template
  Névterek:
  - webdimension.Service
  - webdimension.WebUI










