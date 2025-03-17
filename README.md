# ActiMeet (Server)
Bu repoda, ActiMeet (Activity Meet) projesinin backend reposu yer almaktadır.

## Proje içeriği

### Mimari Yapı
- **Architectural pattern**: Clean Architecture
- **Design Patterns**:
		- Result Pattern
		- Repository Pattern
		- CQRS Pattern
		- UnitOfWork Pattern

### Kullanılan Kütüphaneler
- **MediatR**: CQRS ve mesajlaşma işlemleri için.
- **TS.Result**: Standart sonuç modellemeleri için. (Taner Saydam)
- **Mapster**: Nesne eşlemeleri için.
- **FluentValidation**: Doğrulama işlemleri için.
- **EntityFramework**: ORM (Object-Relational Mapping) için.
- **OData**: Sorgulama ve veri erişiminde esneklik sağlamak için.

## Kurulum ve Kullanım
1. **Depoyu Klonlayın**
	```bash
	git clone https://github.com/actimeet/ActiMeet.Server.git
	cd ActiMeet.Server