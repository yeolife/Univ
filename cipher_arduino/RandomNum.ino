// LCD 모듈 라이브러리를 불러온다
#include <LiquidCrystal.h>

// LCD 설정
// LiquidCrystal(rs, enable, d4, d5, d6, d7)
LiquidCrystal lcd(12, 11, 5, 4, 3, 2);

// 6번 아날로그핀을 X축 입력으로 설정
const int xAxisPin = 0;
// 7번 아날로그핀을 Y축 입력으로 설정
const int yAxisPin = 1;
// 8번 아날로그핀을 Z축 입력으로 설정
const int zAxisPin = 7;

// LED 5개
const int ledA = 6;
const int ledB = 8;
const int ledC = 9;
const int ledD = 10;

// LED값 저장배열
int ledSave[5];
int joySave[5]={0,0,0,0,0};

int i=0;                              // 배열순서용 인자


void setup() {
  randomSeed(analogRead(0));         // 랜덤함수 중복방지용
  // Z축 입력은 디지털 입력으로 설정한다
  pinMode(zAxisPin, INPUT_PULLUP);
  // 16X2 LCD 설정
  lcd.begin(16, 2);
  // 메세지를 표시한다
  lcd.print("20131238");
  lcd.setCursor(0, 1);
  lcd.print("20161256");
  // 1초동안 메세지를 표시한다
  delay(1000);

  // 모든 메세지를 삭제한 뒤
  // X축 Y축 문자를 출력한다
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("RANDOM:");
  lcd.setCursor(0, 1);
  lcd.print("RESULT:");
  lcd.setCursor(15, 1);

  ran();                   // 랜덤함수는 한번만 실행

  pinMode(ledA, OUTPUT);
  pinMode(ledB, OUTPUT);
  pinMode(ledC, OUTPUT);
  pinMode(ledD, OUTPUT);
  Serial.begin(9600);
}

void loop() {
  // X, Y, Z축 값을 읽는다
  int xValue = analogRead(xAxisPin);
  int yValue = analogRead(yAxisPin);
  int zValue = digitalRead(zAxisPin);

  // 그래프를 그리기 위해서 X, Y값을 조절한다
  int xDisplay = map(xValue, 0, 1023, 6, 15);
  int yDisplay = map(yValue, 0, 1023, 6, 15);
  
  if(xValue>=1000) {             // UP값 감지
    output(1,i);
    flash(1);
    i++;                         // 다음 배열로 넘어간다
    delay(300);                  // 채터링 입력방지
  }
  else if(yValue<=100) {         // LEFT값 감지
    output(2,i);
    flash(2);
    i++;
    delay(300);
  }
  else if(yValue>=1000) {        // RIGHT값 감지
    output(3,i);
    flash(3);
    i++;
    delay(300);
  }
  else if(xValue<=100) {         // DOWN값 감지
    output(4,i);
    flash(4);
    i++;
    delay(300);
  }

  if(joySave[4]!=0) {        // 조이스틱 마지막 배열에 값이 들어 왔을 경우
   lcd.setCursor(7,1);
  for(int i=0; i<5; i++) {   // 5개의 배열값을 출력한다
    if(joySave[i]==1){       // Up입력이 들어오면
      lcd.print("U");
    }
    else if(joySave[i]==2){  // Left입력이 들어오면
    lcd.print("L");
    }
    else if(joySave[i]==3){  // Right입력이 들어오면
    lcd.print("R");
    }
    else if(joySave[i]==4){  // Down입력이 들어오면
    lcd.print("D");
    }
  lcd.print(" ");            // LCD출력할때 한칸씩 띄움
    }
    compare();
  }
}

void ran() {                     // 랜덤난수 5개 정수생성 함수
  
  for(int i=0; i<5; i++) {       // 5개의
  ledSave[i]=random(1, 5);       // 1~4의 숫자를 배열에 저장한다
  }
  lcd.setCursor(7,0);
  for(int j=0; j<5; j++) {
    if(ledSave[j]==1) {           // 위
    lcd.print("U");
    }
    else if(ledSave[j]==2) {      // 왼쪽
      lcd.print("L");
    }
    else if(ledSave[j]==3) {      // 오른쪽
      lcd.print("R");
    }
    else if(ledSave[j]==4) {      // 아래
      lcd.print("D");
    }
  lcd.print(" ");
  }
}

void output(int value, int num) {  // 조이스틱 입력값 저장배열함수
  joySave[num]=value;
}

void flash(int f) {                // 조이스틱 입력값에 대한 LED 점멸함수
  if(f==1){                        // 위쪽 입력시 위쪽 빨간 LED 0.3초 점멸
    digitalWrite(ledA,HIGH);    
    delay(300);
    digitalWrite(ledA,LOW);
    }
    else if(f==2) {                // 왼쪽 입력시 왼쪽 초록 LED 0.3초 점멸
    digitalWrite(ledB,HIGH);
    delay(300);
    digitalWrite(ledB,LOW);
    }
    else if(f==3) {                // 오른쪽 입력시 오른쪽 초록 LED 0.3초 점멸
    digitalWrite(ledC,HIGH);
    delay(300);
    digitalWrite(ledC,LOW);
    }
    else if(f==4) {                // 아래쪽 입력시 아래쪽 빨간 LED 0.3초 점멸
    digitalWrite(ledD,HIGH);
    delay(300);
    digitalWrite(ledD,LOW);
    }
}

void compare() {                              // 배열 비교함수
  int comp=0;
  Serial.print("암호: ");
  for(int a=0; a<5; a++) {                    // 시리얼모니터 암호값 출력
  Serial.print(ledSave[a]);
  }
  Serial.println();
  Serial.print("입력: ");
  for(int a=0; a<5; a++) {
  Serial.print(joySave[a]);                   // 시리얼모니터 입력값 출력
  }
  Serial.println();
  
  for(int i=0; i<5; i++) {
    if(ledSave[i]==joySave[i]){               // 문자열 순서와 값 비교
      comp+=1;
    }
  }
  if(comp>=5) {                               // 모든 값이 같으면 성공함수
    sucess();
  }
  else if(comp<5){                            // 하나라도 틀리면 실패함수
    fail();
  }
}

void sucess() {                              // 배열 비교일치 함수
  lcd.clear();                               // 화면 초기화 
  lcd.setCursor(4, 0);
  lcd.print("Compare");                     
  lcd.setCursor(5, 1);
  lcd.print("Sucess");
  Serial.println();
  Serial.print("성공!");
  delay(100000);
}

void fail() {                                 // 배열 불비교일치 함수
  lcd.clear();
  lcd.setCursor(2, 0);
  lcd.print("Incomparable");
  lcd.setCursor(6, 1);
  lcd.print("Fail");
  Serial.println();
  Serial.print("실패!");
  delay(100000);
  }
