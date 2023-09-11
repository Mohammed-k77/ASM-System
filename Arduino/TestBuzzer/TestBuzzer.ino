byte r=13 ;
void setup() {
 pinMode (r,OUTPUT);

 Serial.begin(9600);
}

void loop() {
char x = Serial.read();
if(x =='1')
digitalWrite(r,HIGH);
 else if (x =='2')
 digitalWrite(r,LOW);
}
