package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;

public class ThreadServer implements Runnable{
    private Socket socket;
    public ThreadServer(Socket socket)
    {
        this.socket = socket;
    }

    @Override
    public void run() {
        try (ObjectOutputStream os = new ObjectOutputStream(this.socket.getOutputStream())) {
            try (ObjectInputStream is = new ObjectInputStream(this.socket.getInputStream())) {
                os.writeUTF("ready");
                os.flush();
                int number = is.readInt();
                System.out.println(number);

                os.writeUTF("ready for messages");
                os.flush();
                for (int i = 0; i < number; i++) {
                    Message mess = (Message)is.readObject();
                    System.out.println(mess.getMess());
                }
                os.writeUTF("finish");
                os.flush();

                is.close();
            } catch (ClassNotFoundException e) {
                throw new RuntimeException(e);
            }
        } catch (IOException ex) {
            System.err.println(ex);
        }
        try {
            this.socket.close();
        } catch (IOException var7) {
            var7.printStackTrace();
        }

    }
}
