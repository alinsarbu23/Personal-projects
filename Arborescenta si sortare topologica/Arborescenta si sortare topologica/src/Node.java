import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;

public class Node
{
    private int coordX;
    private int coordY;
    private int number;

    private Color culoare;

    public Node(int coordX, int coordY, int number)
    {
        this.coordX = coordX;
        this.coordY = coordY;
        this.number = number;
        this.culoare = Color.BLACK;
    }

        public Color getCuloare() {
        return culoare;
    }

        public void setCuloare(Color culoare) {
        this.culoare = culoare;
    }


    public int getCoordX() {
        return coordX;
    }
    public void setCoordX(int coordX) {
        this.coordX = coordX;
    }
    public int getCoordY() {
        return coordY;
    }
    public void setCoordY(int coordY) {
        this.coordY = coordY;
    }
    public int getNumber() {
        return number;
    }
    public void setNumber(int number) {
        this.number = number;
    }



    public void desenare_nod(Graphics g, int node_diam)
    {
        g.setColor(Color.RED);
        g.setFont(new Font("TimesRoman", Font.BOLD, 15));
        g.fillOval(coordX, coordY, node_diam, node_diam);
        g.setColor(Color.WHITE);
        g.drawOval(coordX, coordY, node_diam, node_diam);
        if(number < 10)
            g.drawString(((Integer)number).toString(), coordX+13, coordY+20);
        else
            g.drawString(((Integer)number).toString(), coordX+8, coordY+20);
    }


}

