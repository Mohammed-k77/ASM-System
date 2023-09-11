#include <SPI.h>
#include <MFRC522.h>

MFRC522 rf(10,9);
void setup()
{
   Serial.begin(9600);
   SPI.begin();
   rf.PCD_Init();
}

void loop()
{
   //if(rf.isCard()){
    if(rf.readCardSerial()){

      Serial.println("Card ID: ");
      Serial.print(rf.serNum[0]);
      Serial.print(",");
      Serial.print(rf.serNum[1]);
      Serial.print(",");
      Serial.print(rf.serNum[2]);
      Serial.print(",");
      Serial.print(rf.serNum[3]);
      Serial.print(",");
      Serial.print(rf.serNum[4]);
      Serial.println(" ");

    }
   }
   }
