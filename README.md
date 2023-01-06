# Kids-Card-Game
Öğrenme ve ezberleme zorluğu çeken çocuklar için kart eşleştirme oyunu. Çocuklara ezberlemeyi aşılarken eğlendirmeyi ve öğretmeyi hedefleyen bir proje.

FirebaseAuth ve FirebaseDatabase paketleri eklenmelidir.

<br><br>

Firebase Authentication ile email ve şifre kullanılarak giriş yapılır. Üye olma kısmında ayrıca username kısmı bulunmaktadır. Oyunda kullanıcı olmasının sebebi bir skor sistemi bulunmasıdır. Bu şekilde bir rekabet hedeflenmiştir.

<br>

<p float="left">

  <img src="https://user-images.githubusercontent.com/57044969/211082156-678f0c84-80b4-4064-ab54-c6e6455bc913.png"  width="230" />
 
  <img src="https://user-images.githubusercontent.com/57044969/211082709-f27de7c2-f42a-4be6-be84-20aa94c1a13d.png"  width="230" />
  
</p>

<br><br>

Öncelikle kartlar oyun başlangıcında açık olarak başlar ve 3 saniyenin ardından kapanır. Her oyun başlangıcında kart konumları rastgele olarak ayarlanır. Raycast kullanarak tıklanılan kartlar collider ile yakalanıp bu kartın önce script'i üzerinden tıklanma bilgisi True olarak ayarlanıyor. Daha sonra bu kartın isim bilgisi üzerinden bulunarak bu gameobject tutuluyor. Tıklama sayısı 2 olduktan sonra deste tıklanılamaz olarak değiştiriliyor. Ardından tıklanılan kartların tag'leri kontrol edildiğinde eğer taglar uyuşuyorsa önce o hayvanın sesi oynatılır ardından kartlar destroy edilir. Eğer uyuşmuyorlarsa önce bir hata sesi oynatılır ardından kartların tıklanma bilgisi False olur, kartlar tekrar kapanır, deste tıklanılabilir olur ve tıklanma sayısı tekrar 0'a döner.

<br>

<p float="left">

  <img src="https://user-images.githubusercontent.com/57044969/211088218-ea3ead67-a106-40da-bc7c-04fb9803ef6e.png"  width="230" />
 
  <img src="https://user-images.githubusercontent.com/57044969/211088273-86f94ef1-b010-4b16-b1a7-7e6a494bf525.png"  width="230" />
  
  <img src="https://user-images.githubusercontent.com/57044969/211088391-2cd0b7de-4769-4266-9122-26c401e044d8.png"  width="230" />
  
   <img src="https://user-images.githubusercontent.com/57044969/211103657-9ef76c35-96d0-41c2-929f-77aa1cc2faa9.png"  width="230" />
  
</p>

<br><br>

Oyun sonundaki skor, yapılan hata sayısı ve geçen süreyle belirlenir. Oyun 3000 skor ile başlar. Her geçen saniye skorden 5 puan ve her yapılan hata ise skordan 10 puan eksiltmektedir. Oyun tamamlandıktan sonra yapılan skor firebase realtime database'e gönderilir ve kaydedilir. Database'te Kullanıcılar ve Skorlar olarak 2 tablo bulunmaktadır. Skorlar skor tablosunda kullanıcının unique id'sine bağlı şekilde tutulur. Oyunun en son kısmında iste skor tablosu büyükten küçüğe sıralanır.

<br>

<p float="left">

  <img src="https://user-images.githubusercontent.com/57044969/211101809-8eda196f-0b05-4bb6-af93-1846a474872c.png"  width="230" />
 
  <img src="https://user-images.githubusercontent.com/57044969/211101952-a12d17bf-fbca-47f8-8885-3b156bd9a66b.png"  width="230" />
  
</p>


