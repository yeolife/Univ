import java.io.IOException;
import java.net.InetSocketAddress;
import java.nio.ByteBuffer;
import java.nio.channels.ServerSocketChannel;
import java.nio.channels.SocketChannel;
import java.util.Vector;

public class Server {
    static Vector<ClientHandler> ar = new Vector<>(); // 핸들러 벡터
    static int i = 1; // 끝말잇기 시작순번

    // 버퍼내용 송신(클라이언트와 동일)
    private static void sendMsg(SocketChannel channel, String msg) {
        try {
            ByteBuffer buffer = ByteBuffer.allocate(msg.length() + 1);
            buffer.put(msg.getBytes());
            buffer.put((byte)0x00);
            buffer.flip();
            while(buffer.hasRemaining()) channel.write(buffer);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    // 버퍼내용 수신(클라이언트와 동일)
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

    public static void main(String[] args) throws IOException {
        try (ServerSocketChannel sschannel = ServerSocketChannel.open()) { // 서버소캣채널 패시브오픈
            System.out.println("끝말잇기 게임 서버 시작");
            sschannel.configureBlocking(true); // Blocking 모드로 동작
            sschannel.socket().bind(new InetSocketAddress(5000)); // 서버소켓 5000번 대기
            while (true) {
                if(i<3) { // 2명 전까지 대기
                    System.out.println("사용자 접속 대기...");
                }
                else { // 2명 되면 시작
                    System.out.println("게임시작");
                }
                SocketChannel client = sschannel.accept(); // 클라이언트 접속시 채널생성
                String name = recvMsg(client); // 클라이언트로부터 이름정보 얻어옴
                String Tempmsg=""; // 임시저장용 비교변수
                sendMsg(client, name+"#"+i); // 순번 및 본인확인용
                System.out.println("사용자 접속: " + name);
                ClientHandler mtch = new ClientHandler(client, name, i, Tempmsg); // 핸들러 생성
                Thread t = new Thread(mtch); // 스레드 적용
                System.out.println(name + " 을 사용자 리스트에 등록");
                ar.add(mtch); // 벡터에 저장
                t.start(); // 스레드 시작
                i++; // 순번 증가
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}