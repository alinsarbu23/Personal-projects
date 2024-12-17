import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.JFrame;
import javax.swing.SwingUtilities;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

public class Graf {
    private static void initUI(MyPanel panel) {
        JFrame f = new JFrame("Algoritmica Grafurilor - desenare graf");
        f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        f.add(panel);

        JButton buton_salvare = new JButton("Salvare Matrice de Aadiacenta");
        buton_salvare.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                try {
                    panel.matrice_adiacenta("matrice_adiacenta.txt");
                } catch (IOException ex) {
                    throw new RuntimeException(ex);
                }
            }
        });

        JPanel buttonPanel = new JPanel();
        buttonPanel.add(buton_salvare);

        f.add(buttonPanel, "South");
        f.setSize(500, 500);
        f.setVisible(true);
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(new Runnable() {
            public void run() {
                MyPanel panel = new MyPanel();
                initUI(panel);
            }
        });
    }
}