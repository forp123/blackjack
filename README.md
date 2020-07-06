# Blackjack
Windows Forms Project by: Filip Bogdanovski

Oпис на апликацијата:
Апликацијата што ја развивам е класичната игра на среќа Blackjack, која ја прилагодив играта да се одвива помеѓу играчот/корисникот и дилерот/компјутерот.

### Упатство за играње:
Интерфејсот на апликацијата се состои од следните работи:
1) Deal Card(button) - при притискање на ова копче се доделува карта на играчот, ако поените се повеќе од 21 играта престанува за играчот
2) Start(button) - ова копче ја почнува играта на почеток играчот добива две карти, а пак дилерот исто добива две карти но една не е видлива на почетокот на играта
3) Stand(button) - играчот би го притиснал ова копче кога би завршил со својата игра, односно не сака поќе да влече карти, и дилерот продолжува да влече

Исто така во програмата има и две прозорчиња каде се прикажуваат моменталните вкупни вредности на картите кој ги поседуваат играчот и дилерот.

Ако играчот има вкупна вредност на картите која е 21 или е помала од 21 а сепак е поблиску до 21 од дилерот. Тогаш тој е победник.
Во другите случаеви дилерот е победник или играта може да заврши нерешено.

### Структурата на апликацијата е следна:

Целиот шпил од картите се чува во Resources.

Со oваа функција dealCardOnPosition(int position, bool dealer, bool visible) се дели карта

```C#
private void dealCardOnPosition(int position, bool dealer, bool visible)
        {
            if (cards.Count == 0)
            {
                resetGame();
            }

            int cardIndex = random.Next(0, cards.Count - 1);

            int value = cards[cardIndex];
            cards.RemoveAt(cardIndex);

            PictureBox pb = getPictureBox(position, dealer);
            pb.Tag = value;

            if (visible)
            {
                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + value);
            }
            else
            {
                pb.Image = (Image)Properties.Resources.back;
            }
        }
```


Вака се презвзема точната рамка за каде треба да биде поставена картата при влечење:
```C#
 private PictureBox getPictureBox(int position, bool dealer)
        {
            foreach (Control c in Controls)
            {
                string name = dealer ? "pbDealer" : "pictureBox";
                if (c is PictureBox && c.Name == name + position)
                {
                    return (PictureBox)c;
                }
            }
            return null;
        }
 ```
 
 Со  `public int checkValue(int numberCard)` се проверува вредноста на картата и со `private int getSum(bool dealer)` и `private void calculateSums()`
 се пресметуваат моменталните поени на играчот и дилерот.
 
 
 На крај кога ќе се достигнат одредени поени или играта ќе заврши се користи `private void calculateWinner()` за да се види излезот од играта.

 
