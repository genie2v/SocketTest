import java.net.*;
import java.util.Scanner;
import java.io.*;

public class Server2 {

	public static void main(String[] args) throws IOException {
		// TODO Auto-generated method stub
		ServerSocket serverSocket = null;
		Socket socket = null;
		InputStreamReader inputStreamReader = null;
		OutputStreamWriter outputStreamWriter = null;
		BufferedReader bufferedReader = null;
		BufferedWriter bufferedWriter = null;
		String msgFromClient = null;

		serverSocket = new ServerSocket(8000);

		while (true) {
			try {
				socket = serverSocket.accept();

				System.out.println("Client 접속");

				inputStreamReader = new InputStreamReader(socket.getInputStream());
				outputStreamWriter = new OutputStreamWriter(socket.getOutputStream());

				bufferedReader = new BufferedReader(inputStreamReader);
				bufferedWriter = new BufferedWriter(outputStreamWriter);

				Scanner scanner = new Scanner(System.in);

				while (true) {
					msgFromClient = bufferedReader.readLine();

					if (msgFromClient == null)
						break;

					System.out.println("Client: " + msgFromClient);
					
					System.out.print("Receive msg: ");
					String receive = scanner.nextLine();

					bufferedWriter.write(msgFromClient + receive);
					bufferedWriter.newLine();
					bufferedWriter.flush();
				}

				socket.close();
				inputStreamReader.close();
				outputStreamWriter.close();
				bufferedReader.close();
				bufferedWriter.close();

				System.out.println("Client 접속 해제");
				break;

			} catch (IOException e) {
				System.err.println(e);
			}
		}
	}
}
