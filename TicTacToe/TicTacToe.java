/*That's the TicTacToe game 25.11.2023 10:00-15:30*/
import java.awt.*; //Graphics, design
import java.awt.event.*;
import java.util.*; //Data structures
import javax.swing.*; //Main wimdow and buttons

class TicTacToe implements ActionListener {
    private Random random = new Random();
    private JFrame frame = new JFrame(); //fereastra principala
    private JPanel title_panel = new JPanel(); //panou pentru titlu
    private JPanel button_panel = new JPanel(); //panou pentru butoane
    private JLabel text_field = new JLabel();
    private JButton[] buttons = new JButton[9];
    private boolean player1 = true; //daca e randul jucatorului 1, false daca e jucatorul 2

    TicTacToe() {

        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE); //setare fereastra inchidere la x
        frame.setSize(600, 600); //window size
        frame.getContentPane().setBackground(Color.LIGHT_GRAY); /* sau new Color (R G B); */ // setare culoare fundal
        frame.setLayout(new BorderLayout()); //setare dimensiune tabla joc (operatiune default pentru asezarea elementelor mai tarziu, in pagina)
        frame.setVisible(true);

        text_field.setBackground(Color.gray); //culoare fundal text
        text_field.setForeground(Color.white); //culoare text
        text_field.setFont(new Font("Times New Roman", Font.BOLD, 70)); //font, dimensiune si sau boldat
        text_field.setHorizontalAlignment(JLabel.CENTER); // pozitionare
        text_field.setText("X și 0"); //text
        text_field.setOpaque(true); //opac (netransparent)

        title_panel.setLayout(new BorderLayout()); //panou cu text (titlul aici)
        title_panel.setBounds(0, 0 , 800, 100);

        button_panel.setLayout(new GridLayout(3,3));
        button_panel.setBackground(Color.orange);

        for (int i =0; i < 9 ; i++) {
            buttons[i] = new JButton();
            button_panel.add(buttons[i]); //adaugare buton in panou
            buttons[i].setFont(new Font("Calibri", Font.BOLD, 50)); //setare font text din casute
            buttons[i].setFocusable(false); //butoanele nu pot fi accesate
            buttons[i].addActionListener(this); //se intampla ce va fi apelat in action performed

        }

        title_panel.add(text_field);
        frame.add(title_panel, BorderLayout.NORTH);
        frame.add(button_panel);

        first_turn();
    }
    @Override
    public void actionPerformed(ActionEvent e) {

        //actiunea consta in marcarea unei casute cu x si schimbarea titlului (intre randul lui x si al lui 0)
        //asta se va intampla de noua ori

        for(int i =0; i < 9; i++) {

            if(e.getSource() == buttons[i]) {
                if (player1) {
                    if(buttons[i].getText() == "") { //daca casuta e goala
                        buttons[i].setForeground(Color.blue);
                        buttons[i].setText("X");
                        player1 = false;
                        text_field.setText("Rândul lui 0");

                        winner_check();
                    }
                }
                else if(!player1) {
                    if(buttons[i].getText() == "") { //daca casuta e goala
                        buttons[i].setForeground(Color.red);
                        buttons[i].setText("0");
                        player1 = true;
                        text_field.setText("Rândul lui X");

                        winner_check();
                    }
                }
            }

        }

    }
    private void first_turn() {

        try { //timp intre titlu joc si first_turn
            Thread.sleep(2000);
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }

        if(random.nextInt(2) == 0) { //bound 2 valori intre 0 si 1
            player1 = true; //randul jucatorului 1 la start
            text_field.setText("Rândul lui X");
            winner_check();
        }
        else {
            player1 = false; //randul jucatorului 1 la start
            text_field.setText("Rândul lui 0");
            winner_check();
        }
    }
    private void winner_check() {

        int [][] matrice_solutii = {
                {0,1,2}, {3,4,5}, {6,7,8},
                {0,3,6}, {1,4,7}, {2,5,8},
                {0,4,8}, {2,4,6}
        };

        boolean winner = false;
        boolean is_full = true;

        for( int[] combinatie_solutie : matrice_solutii) {

            if(buttons[combinatie_solutie[0]].getText() == "X" && buttons[combinatie_solutie[1]].getText() == "X" && buttons[combinatie_solutie[2]].getText() == "X") {
                x_wins(combinatie_solutie[0], combinatie_solutie[1], combinatie_solutie[2]);
                winner = true;
                break;

            }
            else if(buttons[combinatie_solutie[0]].getText() == "0" && buttons[combinatie_solutie[1]].getText() == "0" && buttons[combinatie_solutie[2]].getText() == "0") {
                zero_wins(combinatie_solutie[0], combinatie_solutie[1], combinatie_solutie[2]);
                winner = true;
                break;
            }

        }

        for(JButton button : buttons) {

            if(button.getText().isEmpty()) {
                is_full = false;
                break;
            }

        }


        if(winner == false && is_full == true) {
            text_field.setText("Remiză");
        }

    }
    private void x_wins(int a, int b, int c) {

        buttons[a].setBackground(Color.green);
        buttons[b].setBackground(Color.green);
        buttons[c].setBackground(Color.green);

        for(int i = 0; i < 9; i++) {
            buttons[i].setEnabled(false);
        }

        text_field.setText("X a câștigat !");
    }
    private void zero_wins(int a, int b, int c) {

        buttons[a].setBackground(Color.green);
        buttons[b].setBackground(Color.green);
        buttons[c].setBackground(Color.green);

        for(int i = 0; i < 9; i++) {
            buttons[i].setEnabled(false);
        }

        text_field.setText("0 a câștigat !");

    }
}

