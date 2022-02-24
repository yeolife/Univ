import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.SocketAddress;
import java.nio.ByteBuffer;
import java.nio.channels.SocketChannel;
import java.util.Scanner;
import java.util.StringTokenizer;

public class Client {
    // 버퍼 송신함수
    private static void sendMsg(SocketChannel channel, String msg) {
        try {
            ByteBuffer buffer = ByteBuffer.allocate(msg.length() + 1); // 메시지의 끝을 알리는 것을 사용하기 위해 + 1
            buffer.put(msg.getBytes()); // String -> 바이트
            buffer.put((byte)0x00); // 0x00: 메시지의 끝을 가리키는 마크
            buffer.flip(); //  읽기모드
            while(buffer.hasRemaining()) channel.write(buffer); // 버퍼의 끝까지 전송
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    // 버퍼 수신함수
    private static String recvMsg(SocketChannel channel) {
        try {
            ByteBuffer buffer = ByteBuffer.allocate(64); // 64바이트 넌다이렉트버퍼
            String msg = "";
            while(channel.read(buffer) > 0) {
                char byteRead = 0x00; // 버퍼의 끝
                buffer.flip(); // 읽기모드
                while(buffer.hasRemaining()) { // 포지션과 리미트가 같은지(내용이 있는지)
                    byteRead = (char) buffer.get(); // 하나씩 꺼내서
                    if(byteRead == 0x00) break;
                    msg += byteRead; // 저장
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
    private static void show(String msg) {
        System.out.println(msg);
    }

    public static void main(String[] args) throws IOException {
        Scanner sc = new Scanner(System.in);
        String message;
        String Checkmsg;
        String who;
        String msg;
        boolean start = false;
        SocketAddress address = new InetSocketAddress("127.0.0.1", 5000);
        try (SocketChannel channel = SocketChannel.open(address)) {
            channel.configureBlocking(true);
            System.out.println("게임서버 접속 완료");
            System.out.print("이름 >> ");
            String name = sc.nextLine();
            sendMsg(channel, name);

            while (true) {
                message=recvMsg(channel);
                if(!message.contains("#")) continue; // 수신처가 없는 메시지인 경우
                StringTokenizer tokenizer = new StringTokenizer(message, "#");
                who = tokenizer.nextToken(); // 자신
                msg = tokenizer.nextToken(); // 순번
                if(who.equals(name)) {
                    start = true;
                    break;
                }
            }
            while (start) {
                if(msg.equals("1")) { // 첫번째 클라이언트면
                    System.out.print("단어 >> "); // 단어송신부터 시작
                    message = sc.nextLine();
                    if (message.equals("logout")) {
                        sendMsg(channel, message);
                        break;
                    }
                    sendMsg(channel, message);
                    Checkmsg = recvMsg(channel);
                    show(Checkmsg);
                    if(Checkmsg.contains("won!!")) {
                        sendMsg(channel,"Game Over!!");
                        break;
                    }
                }
                else { // 두번째 클라이언트면
                    Checkmsg = recvMsg(channel); // 수신부터 시작
                    show(Checkmsg);
                    if(Checkmsg.contains("won!!")) {
                        sendMsg(channel,"Game Over!!");
                        break;
                    }
                    System.out.print("단어 >> ");
                    message = sc.nextLine();
                    if (message.equals("logout")) {
                        sendMsg(channel, message);
                        break;
                    }
                    sendMsg(channel, message);
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
