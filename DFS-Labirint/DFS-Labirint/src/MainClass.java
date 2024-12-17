import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.Scanner;

public class MainClass {
    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            JFrame window = new JFrame("Labirint");
            JMenuBar meniu = new JMenuBar();

            //JMenu fileMenu = new JMenu("File");
            JMenuItem exitItem = new JMenuItem("Inchide");

            exitItem.addActionListener(new ActionListener() {
                public void actionPerformed(ActionEvent e) {
                    System.exit(0);
                }
            });

            //fileMenu.add(exitItem);
           // meniu.add(fileMenu);
            window.setJMenuBar(meniu);


            String caleFisierLabirint = "C:\\Users\\Alin Sârbu\\OneDrive\\Desktop\\AG\\A\\src\\Labirint.txt";

            Labirint labirint = new Labirint(caleFisierLabirint);


            Scanner scanner = new Scanner(System.in);
            System.out.print("Introduceți linia punctului de intrare albastru: ");
            int x = scanner.nextInt();
            System.out.print("Introduceți coloana punctului de intrare albastru: ");
            int y = scanner.nextInt();


            labirint.set_start(new Point(x, y));

            Parcurgere parcurgere_labirint = new Parcurgere(labirint);
            Panou labirintUI = new Panou(labirint, parcurgere_labirint);
            window.add(labirintUI);
            window.setSize(labirint.get_linii() * 30, labirint.get_coloane() * 30);
            window.setDefaultCloseOperation(JFrame.DO_NOTHING_ON_CLOSE); // Schimbăm comportamentul de închidere


            window.addWindowListener(new WindowAdapter() {
                @Override
                public void windowClosing(WindowEvent e) {
                    labirintUI.stop_animatie();
                    window.dispose();
                }
            });

            window.setVisible(true);


            labirintUI.start_animatie();
        });
    }
}
