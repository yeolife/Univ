# WordChain

정의 

Java NIO의 TCP/IP Socket 통신으로 서버는 요청을 대기하다가 2명의 클라이언트가 연결 요청을 하면, 핸들러를 통해 중재하고 클라이언트끼리 Bytebuffer를 사용하여 영단어 끝말잇기를 할 수 있게 만들어 봤다.


작동순서

1. 서버의 ServerSocketChannel이 Blocking 모드로 5000번 port에서 클라이언트의 요청을 대기한다.


2. 각 클라이언트가 접속하면 SocketChannel과 ClientHandler를 생성한다. 


3. 클라이언트 2명이 서버에 연결되면 첫번째 클라이언트가 메시지를 자신의 핸들러에게 송신한다.


4. 클라이언트 핸들러에서는 문자열의 끝을 비교해 주고 수신할 클라이언트에 메시지를 전달해 준다. 


5. 수신 클라이언트는 상대의 핸들러로부터 메시지를 받고 자신의 핸들러로 메시지를 송신한다. 


6. 반복하다가 문자열의 끝과 다르면 종료해 준다.



결과

![image](https://user-images.githubusercontent.com/82012857/176306088-5259b6eb-3fff-46f1-b03c-b78feee03eb2.png)

서버

![image](https://user-images.githubusercontent.com/82012857/176306107-cdf369a7-7ce9-4642-817a-a6e7070e627d.png) 
![image](https://user-images.githubusercontent.com/82012857/176306112-3bf4cf09-fd19-4ecd-8a69-933ce44cb99c.png)

유저1 유저2
