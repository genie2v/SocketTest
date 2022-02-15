package DB;

import java.io.*;
import java.net.*;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class ServerDB {

	public static void main(String[] args) throws IOException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement pstm = null;
		ResultSet rs = null;

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

				conn = DBConnection.getConnection();

				inputStreamReader = new InputStreamReader(socket.getInputStream());
				outputStreamWriter = new OutputStreamWriter(socket.getOutputStream());

				bufferedReader = new BufferedReader(inputStreamReader);
				bufferedWriter = new BufferedWriter(outputStreamWriter);

				while (true) {
					String response = null;
					msgFromClient = bufferedReader.readLine();

					if (msgFromClient == null)
						break;

					System.out.println("Client: " + msgFromClient);
					try {
						String select = "SELECT B FROM TESTA WHERE A = '" + msgFromClient + "'";

						pstm = conn.prepareStatement(select);
						rs = pstm.executeQuery();

						while (rs.next()) {
							response = rs.getString("B");
						}

						System.out.println("Server: " + response);

						bufferedWriter.write(response);
						bufferedWriter.newLine();
						bufferedWriter.flush();
					} catch (SQLException e) {
						// TODO: handle exception
						e.printStackTrace();
					} 
				}

				try {
					if (pstm != null)
						pstm.close();
					if (conn != null)
						conn.close();
					if (rs != null)
						rs.close();
					System.out.println("DB 접속 해제");
				} catch (Exception e) {
					e.printStackTrace();
				}
				
				socket.close();
				inputStreamReader.close();
				outputStreamWriter.close();
				bufferedReader.close();
				bufferedWriter.close();

				System.out.println("Client 접속 해제");
				break;
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}

}
