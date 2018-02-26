/*
 재작자: 여상준(Yeo Sang-Jun)
 아두이노 소스코드 공개 버전
 */

//---사용자 임의 설정 가능---//
const int Apin = 13; //버튼 1
const int Bpin = 12; //버튼 2
const int Cpin = 11; //버튼 3
const int Dpin = 10; //버튼 4
const int Epin = 9; //버튼5
const int plus = 8; //전원 공급 핀
const int chattering = 0; //ms단위로 버튼 채터링이 멈출때 까지의 대기시간 입니다. 대부분 5ms, 10ms로 설정하면 됩니다.


//---사용자 임의 설정 불가---//
int a,b,c,d,e = 0; //검수 전용(수정금지)
int A,B,C,D,E = 0; //검수 전용(수정금지)

void setup() {
  pinMode(Apin,INPUT_PULLUP);
  pinMode(Bpin,INPUT_PULLUP);
  pinMode(Cpin,INPUT_PULLUP);
  pinMode(Dpin,INPUT_PULLUP);
  pinMode(Epin,INPUT_PULLUP);
  pinMode(plus, OUTPUT);
  Serial.begin(9600); //통신속도 수정금지
}
void loop() {
  if(digitalRead(Apin)==LOW){
        if(A==0){Serial.println("A"); a=0; A=1; delay(chattering);}
     }else if(a==0) {Serial.println("1"); a=1; A=0; delay(chattering);}
     
  if(digitalRead(Bpin)==LOW){
        if(B==0){Serial.println("B"); b=0; B=1; delay(chattering);}
     }else if(b==0) {Serial.println("2"); b=1; B=0; delay(chattering);}
     
  if(digitalRead(Cpin)==LOW){
       if(C==0){Serial.println("C"); c=0; C=1; delay(chattering);}
     }else if(c==0) {Serial.println("3"); c=1; C=0; delay(chattering);}
     
  if(digitalRead(Dpin)==LOW){
        if(D==0){Serial.println("D"); d=0; D=1; delay(chattering);}
     }else if(d==0) {Serial.println("4"); d=1; D=0; delay(chattering);}
     
  if(digitalRead(Epin)==LOW){
        if(E==0){Serial.println("E"); e=0; E=1; delay(chattering);}
     }else if(e==0) {Serial.println("5"); e=1; E=0; delay(chattering);}
     Serial.flush(); //사실 없어도 큰 상관없음
}
