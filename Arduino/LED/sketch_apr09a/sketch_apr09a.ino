byte r=13 , y = 12 , g= 11;
void setup() {
 pinMode (r,OUTPUT);
 pinMode (y,OUTPUT);
 pinMode (g,OUTPUT);
 Serial.begin(9600);
}

void loop() {
char x = Serial.read();
if(x =='1')
digitalWrite(r,HIGH);
 else if (x =='2')
 digitalWrite(r,LOW);

 
if(x =='3')
digitalWrite(y,HIGH);
 else if (x =='4')
 digitalWrite(y,LOW);

 
 if(x =='5')
digitalWrite(g,HIGH);
 else if (x =='6')
 digitalWrite(g ,LOW);
 
}
