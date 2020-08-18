Asp.Net Mvc - Sinema Uygulaması

 * Asp.net Mvc (4.7) ile oluşturulmuştur.
 * Asp.net Web API Mvc (4.7) ile oluşturulmuştur.
  
Katmanlı mimari yapısı ile proje geliştirilmiştir. Mimari 5 katmandan oluşmaktadır:

- Cinema.Api
- Cinema.Api.Business
- Cinema.Api.Data
- Cinema.Web
- Cinema.Web.Business

Proje katmanlarından kısaca bahsedecek olursak;

- Cinema.Api : Web uygulamasını besleyecek olan API katmanı kodlanmıştır. Asp.Net Mvc Web API (4.7) ile kodlanmıştır. Web katmanının Http isteklerinin tamamı buraya yapılacaktır. Token sayesinde yetki kontolleri yapılarak, istek atan kullanıcının sadece yetkisi kadar bilgilere erişimi sağlanmaktadır.
- Cinema.Api.Business : Sql veri tabanı bağlantılarının sağlandığı, isteklerin atıldığı katmandır. Paginated algoritmaları kullanılarak, veri tabanındaki tüm verilerin aktarımı yerine sayfalı şekilde aktarılması sağlanmıştır. Ayrıyeten, Cinema.Api katmanının tüm isteklerindeki "ekleme,güncelleme,silme ve listeleme" işlevlerinin alt yapısının oluşturulduğu katmandır.
- Cinema.Api.Data : Sql veri tabanındaki tabloların bulunduğu katmandır. Tablo isimleri ve property'lerini içermektedir. Ayrıyeten Entity Framework ile Sql bağlantılarının sağlandığı "DbConnection" bilgilerinin bulunduğu katmandır.
- Cinema.Web : Admin işlevlerin yapıldığı katmandır. Asp.Net Mvc(4.7) teknolojisi ile kodlanmıştır. Web katmanından yönetim işlevleri ayarlanıp, veri ekleme, silme , düzenleme ve listeleme işlevlerinin yapılması sağlanır. Ayrıyeten kullanıcılara erişim yetkilerinin verilmesi veya silinmesi işlevlerinin ayarlandığı katmandır. Buradaki işlevlerden biri de Login olan kişilerin yetkisi dahilinde bulunduğu işlevleri yapabilmesidir. Örneğin, film ekleme, silme ve listeleme profiline sahip kullanıcı sadece kendi yetkisi dahilindeki işlevleri yapabilir. Buradaki işlevler de yetki grupları oluşturulup, yetki gruplarına kişiler eklenmektedir. Bu sayede Login olan kişinin yetkisi bulunduğu kısımlara erişimi sağlanmaktadır.
- Cinema.Web.Business : Web katmanının beslendiği katmandır. Session bilgisinin tutulduğu ve Api'ye istekler sırasında Token bilgisi ile erişimin sağlandığı katmandır. HttpClient ile Api'ye erişim sağlanmaktadır.

Kullanılan Teknolojiler:

- Asp.Net Mvc(4.7)
- Asp.Net Mvc Web API(4.7)
- Sql
- Entity Framework
