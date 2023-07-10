import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.channels.SocketChannel;
import java.util.Scanner;

public class ClientHandler implements Runnable {
    Scanner sc = new Scanner(System.in);
    SocketChannel client;
    private String name; // 이름
    private int i; // 순번
    private String Tempmsg; // 상대 메시지와 비교할 임시변수
    boolean isloggedin; // 로그인 여부

    public ClientHandler(SocketChannel client, String name, int i, String Tempmsg) {
        this.name = name;
        this.client = client;
        this.i = i;
        this.Tempmsg = Tempmsg;
        isloggedin = true;
    }
    // 버퍼 송신함수(클라이언트와 동일)
    private static void sendMsg(SocketChannel channel, String msg) {
        try {
            ByteBuffer buffer = ByteBuffer.allocate(msg.length() + 1); // 메시지의 끝을 알리는 것을 사용하기 위해 + 1
            buffer.put(msg.getBytes());
            buffer.put((byte)0x00); // 0x00: 메시지의 끝을 가리키는 마크
            buffer.flip();
            while(buffer.hasRemaining()) channel.write(buffer);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    // 버퍼 수신함수(클라이언트와 동일)
    private static String recvMsg(SocketChannel channel) {
        try {
            ByteBuffer buffer = ByteBuffer.allocate(64);
            String msg = "";
            while(channel.read(buffer) > 0) {
                char byteRead = 0x00;
                buffer.flip();
                while(buffer.hasRemaining()) {
                    byteRead = (char) buffer.get();
                    if(byteRead == 0x00) break;
                    msg += byteRead;
                }
                if(byteRead == 0x00) break;
                buffer.clear();
            }
            return msg;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return "";
    }

    // 첫번째 문자열의 마지막 문자와 두번째 문자열의 첫번째 문자 비교 함수
    public boolean compare(String word1, String word2) {
        int lastIndex = word1.length()-1; // 첫번째 문자열의 마지막 index
        char lastChar = word1.charAt(lastIndex); // 마지막 index값 추출
        char firstChar = word2.charAt(0); // 두번째 문자열의 첫번째값 추출
        if(lastChar == firstChar) // 같으면
        {
            return true; // 끝말잇기 계속
        }
        else { // 다르면
            return false; // 끝말잇기 끝
        }
    }

    private static void show(String msg) {
        System.out.println(msg);
    }
    // 클라이언트에게 메시지를 받은 다음 다른 클라이언트에게 메시지 전송하기
    @Override
    public void run() {
        String msg="";
        String Checkmsg="";
        boolean result = true;
        while(true) {
            try {
                String names = name + " : "; // 자신의 이름
                msg = recvMsg(client);

                Tempmsg = msg;
                if (msg.equals("logout")) { // 로그아웃이면
                    isloggedin = false;
                    client.close(); // 클라이언트 종료
                    break;
                }
                for (ClientHandler handler : Server.ar) { // 벡터내의 클라이언트 중
                    if (!handler.name.equals(name) && isloggedin) { // 본인이 아닌 상대에게
                        if(!handler.Tempmsg.equals("")) { // 두번째 문자열이 null이 아니면
                            result = compare(handler.Tempmsg, msg); // 매개변수: 상대방의 문자열, 자신의 문자열 비교
                            if(!result) { // 자신이 끝말잇기 실패시
                                show(names + msg); // 서버에서 확인
                                System.out.println(handler.name + " is won!!"); // 상대방이 이김
                                sendMsg(client, handler.name + " is won!!"); // 자신에게 결과 통보
                                Checkmsg = recvMsg(client);
                                if(Checkmsg.equals("Game Over!!")) {
                                    sendMsg(handler.client, names + msg + "\n" + handler.name + " is won!!"); // 상대방에게 결과통보
                                    isloggedin = false; // 자신 로그아웃
                                    handler.isloggedin = false; // 상대방도 로그아웃
                                    break;
                                }
                            }
                            else {
                                sendMsg(handler.client, names + msg); // 자신의 이름,메시지 전송
                                show(names + msg);
                            }
                        } else {
                            sendMsg(handler.client, names + msg); // 자신의 이름,메시지 전송
                            show(names + msg);
                        }
                    }
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
