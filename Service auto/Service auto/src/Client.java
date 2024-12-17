public class Client {
    private int clientId;
    private String nume;
    private String prenume;
    private String varsta;
    private String data_nasterii;
    private String telefon;
    private String activ;

    public Client(String nume, String prenume, String varsta, String data_nasterii, String telefon, String activ) {
        this.nume = nume;
        this.prenume = prenume;
        this.varsta = varsta;
        this.data_nasterii = data_nasterii;
        this.telefon = telefon;
        this.activ = activ;
    }

    public Client(String nume, String prenume, String varsta, String data_nasterii, String telefon) {
        this.nume = nume;
        this.prenume = prenume;
        this.varsta = varsta;
        this.data_nasterii = data_nasterii;
        this.telefon = telefon;
    }

    public int getClientId() {
        return clientId;
    }
    public void setClientId(int clientId) {
        this.clientId = clientId;
    }
    public String getNume() {
        return nume;
    }
    public void setNume(String nume) {
        this.nume = nume;
    }
    public String getPrenume() {
        return prenume;
    }
    public void setPrenume(String prenume) {
        this.prenume = prenume;
    }
    public String getVarsta() {
        return varsta;
    }
    public void setVarsta(String varsta) {
        this.varsta = varsta;
    }
    public String getData_nasterii() {
        return data_nasterii;
    }
    public void setData_nasterii(String data_nasterii) {
        this.data_nasterii = data_nasterii;
    }
    public String getTelefon() {
        return telefon;
    }
    public void setTelefon(String telefon) {
        this.telefon = telefon;
    }
    public String getActiv() {
        return activ;
    }
    public void setActiv(String activ) {
        this.activ = activ;
    }
}
