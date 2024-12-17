import java.awt.Color;
import java.awt.Graphics;

public class Arc {
    private Node startNode;
    private Node endNode;
    private boolean passesThroughNodes;

    private boolean este_orientat;

    private boolean isDirected;

    public Arc(Node startNode, Node endNode)
    {
        this.startNode = startNode;
        this.endNode = endNode;
    }

    public Arc(Node startNode, Node endNode, boolean isDirected) {
        this.startNode = startNode;
        this.endNode = endNode;
        this.isDirected = isDirected;
    }

    public boolean isDirected() {
        return isDirected;
    }



    public boolean get_este_orientat() {
        return este_orientat;
    }

    public void drawArc(Graphics g) {
        if (startNode != null && endNode != null) {
            g.setColor(Color.RED);
            g.drawLine(startNode.getCoordX(), startNode.getCoordY(), endNode.getCoordX(), endNode.getCoordY());
        }
    }

    public boolean passesThroughNodes() {
        return passesThroughNodes;
    }

    public Node getStartNode() {
        return startNode;
    }

    public Node getEndNode() {
        return endNode;
    }

    public boolean isValid() {
        return startNode != null && endNode != null;
    }


}
