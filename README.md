# Flappy Bird

Flappy Bird는 클릭으로 새가 점프하여 파이프 사이를 넘는 게임이다.

간단하면서도 따라 만들기가 쉬워 유니티를 입문할 때 만들어 보기 좋은 게임이다.

기본적인 틀만 구현했는데 좀 더 편리하게 개선하고 난이도 있게 기능을 추가해 보았다.

![0](https://user-images.githubusercontent.com/82012857/176310639-e744ccee-1285-4b66-a87b-fb616403b78d.gif)

<h3>구현한 기능</h3>

1. 버튼을 누르자마자 시작되면 새가 추락하지 않게 준비 패널을 만들어 대기할 수 있게 만들기

![1](https://user-images.githubusercontent.com/82012857/176311180-1e6e2d36-86a4-4b2c-82c5-00a2428e883e.gif)

2. 게임 오버 시 RE 버튼으로 재시작하거나 EXIT 버튼으로 첫 화면으로 돌아갈 수 있게 만들기

![2](https://user-images.githubusercontent.com/82012857/176311289-8b04c062-8fb9-4601-92fd-e968dace46cd.gif)

3. 점수 5점을 넘어가면 챕터 2로 넘어가기

![3](https://user-images.githubusercontent.com/82012857/176311390-a82e28df-ec8a-44cf-8422-48c290488fb3.gif)

4. 챕터 2에서는 빨간 파이프에 부딪히면 게임 오버되게 난이도를 높이기(대신 점수 2배)

![4](https://user-images.githubusercontent.com/82012857/176311478-a4d9a32f-788e-4c04-9d6f-2dec3769c3fd.gif)

5. 점수 5점을 넘기면 챕터 3로 넘어가기

![5](https://user-images.githubusercontent.com/82012857/176311580-66f8bfc6-d937-4f99-a941-d38e6bc4080b.gif)

6. 챕터 3에서는 경고 메시지와 함께 움직이는 파이프가 랜덤한 시간으로 생성됨

![6](https://user-images.githubusercontent.com/82012857/176311679-49feec11-aa7a-4070-83bd-39e4963d3f50.gif)

7. 점수 제한이 없게 하고 파이프에 걸리면 점수판으로 기록을 보여줌

![7](https://user-images.githubusercontent.com/82012857/176311781-9c8bf0f1-dbfe-450a-a442-b57cdc0b357c.gif)

<br><hr>

<h3>출처</h3>

<h4>sprite</h4>

전체 https://www.spriters-resource.com/mobile/flappybird/sheet/59894/

밤배경 http://pixeljoint.com/pixelart/91663.html

축하 https://www.pngwing.com/ko/free-png-bwnnv

버튼 https://www.pngwing.com/ko/free-png-bdbww

<h4>sound</h4>

코인 https://stylemoov.tistory.com/31

박수 https://m.blog.naver.com/PostView.nhn?blogId=love-he&logNo=220683836335&proxyReferer=https:%2F%2Fwww.google.co.kr%2F

<h4>script</h4>

GameManager.cs: 게임을 관리하는 가장 핵심적인 스크립트이다. 게임 시작 버튼 클릭 시 Coroutine함수로 파이프를 랜덤한 위치에 계속해서 만들어주고 시작 패널을 없애준다. 또한 Update함수로 실시간으로 점수를 갱신해 준다.

flyButton.cs: 마우스 우 클릭으로 Player을 y축 방향으로 8f만큼 점프 시켜주는 스크립트이다. 점프할 때마다 z값을 30f만큼 회전시켜준다. 또한 Pipe의 위, 아래와 Floor에 부딪히면 게임을 종료 시켜준다.

pipeMove.cs: 파이프가 x축 방향으로 4f만큼의 속도로 이동하게 해주는 스크립트이다. 파이프가 맵에서 벗어날 시 파이프를 삭제해 준다.

pipeMoveY.cs: 챕터 3의 움직이는 파이프를 위한 스크립트이다.

ScoreUpdateDetect.cs: 파이프 사이의 ScoreZone과 Player가 충돌 시 점수 1점을 부여해 주는 스크립트이다. OnTriggerExit2D 함수를 사용했기 때문에 물리적 접촉이 아닌 통과할 때마다 체크하는 것이므로 ScoreZone에 Is Trigger을 체크한다.

EndPanelManager.cs: 게임 종료 시의 패널을 관리하는 스크립트이다. GameManager 스크립트로부터 점수를 받아와서 최고 점수를 갱신해 주거나 점수별로 메달을 확인해 준다. 또한 종료 후 OK 버튼을 누르면 초기 화면으로 돌아가면서 점수를 초기화한다.

Scene.cs: 챕터 1,2,3 간의 전환을 위한 스크립트이다.

EasyScoreUpdate.cs: 초록색 일반 파이프를 넘을 때 점수를 체크하기 위한 스크립트이다.
