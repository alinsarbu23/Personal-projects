import java.util.*; //data structures
import java.awt.*; //design
import java.awt.event.*; //action event
import javax.swing.*; //animation
import java.util.List;
public class Labirint implements ActionListener {
    private JFrame window = new JFrame("Labirint escape - joc cu interfață grafică (author: Alin Sârbu™)");
    private JPanel panou_text = new JPanel();
    private JLabel text = new JLabel();
    private int linii;
    private int coloane;
    private JPanel panou_tabla = new JPanel();
    private JButton[] tabla;
    private Point start;
    private Point end;
    int indice_destinatie;
    private int[][] matrice_tabla;
    Labirint() {

        citire_dimensiuni();

        tabla = new JButton[linii * coloane];

        text.setBackground(Color.lightGray);
        text.setForeground(Color.DARK_GRAY);
        text.setFont(new Font("Aptos", Font.BOLD, 30));
        text.setHorizontalAlignment(SwingConstants.CENTER);
        text.setText("Labirint escape");
        text.setOpaque(true);

        panou_text.setLayout(new BorderLayout());
        panou_text.setBounds(0, 0, 600, 100);

        panou_tabla.setLayout(new GridLayout(linii, coloane));
        panou_tabla.setBackground(new Color(239, 235, 237));

        for (int i = 0; i < linii * coloane; i++) {
            tabla[i] = new JButton();
            tabla[i].setFont(new Font("Calibri", Font.BOLD, 60));
            tabla[i].setFocusable(false);
            panou_tabla.add(tabla[i]);
            tabla[i].addActionListener(this);
        }

        panou_text.add(text);
        window.add(panou_text, BorderLayout.NORTH);
        window.add(panou_tabla);

        for (JButton button : tabla) {
            button.addActionListener(this);
        }

        start_game();

    }

    private void citire_dimensiuni() {
        JTextField linii = new JTextField(5); // text
        JTextField coloane = new JTextField(5);

        JPanel panou = new JPanel(); // panou

        panou.add(new JLabel("Introduceți numărul de linii:\n"));
        panou.add(linii);

        panou.add(Box.createVerticalStrut(10));

        panou.add(new JLabel("Introduceți numărul de coloane:\n"));
        panou.add(coloane);

        int result = JOptionPane.showConfirmDialog(null, panou, "Introduceți dimensiunile tablei", JOptionPane.OK_CANCEL_OPTION, JOptionPane.PLAIN_MESSAGE);

        if (result == JOptionPane.OK_OPTION) {

            try {
                if (linii.getText().isEmpty() || linii.getText().isEmpty()) {
                    this.linii = 1;
                    this.coloane = 1;
                } else if (!linii.getText().isEmpty() && !linii.getText().isEmpty()) {
                    this.linii = Integer.parseInt(linii.getText());
                    this.coloane = Integer.parseInt(coloane.getText());

                    if (this.linii < 0 || this.coloane < 0) {
                        System.exit(0);
                    }

                    else if(this.linii > 8 || this.coloane > 8) {
                        JOptionPane.showMessageDialog(null, "Valorile depășesc dimensiunea maximă");
                        System.exit(0);
                    }
                }

                openWindow();

            } catch (NumberFormatException e) {
                JOptionPane.showMessageDialog(null, "Dimensiunile nu sunt valide !\n");
                System.exit(0);
            }
        } else {
            System.exit(0);
            openWindow();
        }
    }
    private void openWindow() {
        window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        window.setSize(700, 700);
        window.getContentPane().setBackground(new Color(117, 235, 184));
        window.setLayout(new BorderLayout());
        window.setVisible(true);
    }
    private void start_game() {

        try {
            Thread.sleep(2000);
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }

        text.setText("Alegeti punctul de start");

    }

    @Override
    public void actionPerformed(ActionEvent e) {
        JButton clickedButton = (JButton) e.getSource();

        if (text.getText().equals("Labirint escape")) {
            clickedButton.setBackground(Color.GREEN);
            text.setText("Alegeti punctul de start");
        } else if (text.getText().equals("Alegeti punctul de start")) {
            int index = Arrays.asList(tabla).indexOf(clickedButton);
            int linie = index / coloane;
            int coloana = index % coloane;

            start = new Point(linie, coloana);
            text.setText("Ajungeți la destinație!");

            Random random = new Random();
            Point randomPoint;

            do {
                randomPoint = new Point(random.nextInt(linii), random.nextInt(coloane));
            } while (randomPoint.equals(start));

            end = randomPoint;
            indice_destinatie = end.x * coloane + end.y;
            tabla[indice_destinatie].setBackground(Color.BLUE);

            matrice_tabla = generare_matrice(start, end); // Aici actualizăm matricea generată
        } else {
            int index = Arrays.asList(tabla).indexOf(clickedButton);
            int linie = index / coloane;
            int coloana = index % coloane;

            if (linie == end.x && coloana == end.y) {
                clickedButton.setBackground(Color.BLUE);
                text.setText("Ați ajuns la destinație! Ați câștigat!");
            } else if (matrice_tabla[linie][coloana] == 0) {
                clickedButton.setBackground(Color.RED);
                text.setText("Ai pierdut!");
            } else {
                clickedButton.setBackground(Color.GREEN);
            }
        }
    }

    //that's it

    private int[][] generare_matrice(Point start, Point end) {
        int[][] matrice_tabla = new int[linii][coloane];

        Random random = new Random();
        Point current = new Point(start);

        while (!current.equals(end)) {
            matrice_tabla[current.x][current.y] = 1;

            List<Point> possibleMoves = new ArrayList<>();

            int[] directiiLinie = { -1, 1, 0, 0 };
            int[] directiiColoana = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++) {
                int newLinie = current.x + directiiLinie[i];
                int newColoana = current.y + directiiColoana[i];

                if (newLinie >= 0 && newLinie < linii && newColoana >= 0 && newColoana < coloane &&
                        matrice_tabla[newLinie][newColoana] == 0) {
                    possibleMoves.add(new Point(newLinie, newColoana));
                }
            }

            if (possibleMoves.isEmpty()) {
                // Dacă nu mai există mutări posibile, resetăm drumul și începem din nou
                current = new Point(start);
            } else {
                // Alegem o mutare aleatoare din cele posibile
                current = possibleMoves.get(random.nextInt(possibleMoves.size()));
            }
        }

        // Marcam ultima poziție (destinația)
        matrice_tabla[end.x][end.y] = 1;

        return matrice_tabla;
    }



    public static void main(String[] args) {

        Labirint labirint = new Labirint();


    }


}