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

Raycast kullanarak tıklanılan kartlar collider ile yakalanıp bu kartın önce script'i üzerinden tıklanma bilgisi True olarak ayarlanıyor. Daha sonra bu kartın isim bilgisi üzerinden bulunarak bu gameobject tutuluyor. Tıklama sayısı 2 olduktan sonra deste tıklanılamaz olarak değiştiriliyor. Ardından tıklanılan kartların tag'leri kontrol edildiğinde eğer taglar uyuşuyorsa kartlar destroy edilir. Eğer uyuşmuyorlarsa kartların tıklanma bilgisi False olur, kartlar tekrar kapanır, deste tıklanılabilir olur ve tıklanma sayısı tekrar 0'a döner.

<br>

<p float="left">

  <img src="https://user-images.githubusercontent.com/57044969/211088218-ea3ead67-a106-40da-bc7c-04fb9803ef6e.png"  width="230" />
 
  <img src="https://user-images.githubusercontent.com/57044969/211088273-86f94ef1-b010-4b16-b1a7-7e6a494bf525.png"  width="230" />
  
  <img src="https://user-images.githubusercontent.com/57044969/211088391-2cd0b7de-4769-4266-9122-26c401e044d8.png"  width="230" />
  
</p>
