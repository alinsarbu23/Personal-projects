import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class DBUtils {
    private static final String URL = "jdbc:mysql://localhost:3306/service_auto";
    private static final String USER = "root";
    private static final String PASSWORD = "1q2w3e4r";
    public static Connection getConnection() {
        try {
            return DriverManager.getConnection(URL, USER, PASSWORD);
        } catch (SQLException e) {
            e.printStackTrace();
            throw new RuntimeException("Nu s-a putut stabili conexiunea la baza de date.", e);
        }
    }
    public static void closeConnection(Connection connection) {
        if (connection != null) {
            try {
                connection.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }


    public static List<Client> getAllActiveClients() {
        Connection conn = getConnection();
        List<Client> clients = new ArrayList<>();

        try (CallableStatement callableStatement = conn.prepareCall("{CALL GetAllActiveClients()}")) {
            try (ResultSet resultSet = callableStatement.executeQuery()) {
                while (resultSet.next()) {
                    int clientId = resultSet.getInt("ClientId");
                    String nume = resultSet.getString("Nume");
                    String prenume = resultSet.getString("Prenume");
                    String varsta = resultSet.getString("Varsta");
                    String dataNasterii = resultSet.getString("Data_Nasterii");
                    String telefon = resultSet.getString("Telefon");
                    String active = resultSet.getString("Active");

                    Client client = new Client(nume, prenume, varsta, dataNasterii, telefon, active);
                    clients.add(client);
                }
            }
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
        return clients;
    }
    public static void insertClient(String nume, String prenume, int varsta, String dataNasterii, String telefon) {
        Connection conn = getConnection();

        try (CallableStatement callableStatement = conn.prepareCall("{CALL InsertClient(?, ?, ?, ?, ?)}")) {
            callableStatement.setString(1, nume);
            callableStatement.setString(2, prenume);
            callableStatement.setInt(3, varsta);
            callableStatement.setString(4, dataNasterii);
            callableStatement.setString(5, telefon);

            callableStatement.executeUpdate();
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
    }


    public static List<Masina> getAllActiveMasini() {
        Connection conn = getConnection();
        ArrayList<Masina> masini = new ArrayList<>();

        try (CallableStatement callableStatement = conn.prepareCall("{CALL GetAllActiveMasini()}")) {
            try (ResultSet resultSet = callableStatement.executeQuery()) {
                while (resultSet.next()) {
                    int masinaId = resultSet.getInt("masinaId");
                    String marca = resultSet.getString("Marca");
                    String model = resultSet.getString("Model");
                    String an_fabricatie = resultSet.getString("AnFabricatie");
                    String posesor = resultSet.getString("Posesor");
                    String active = resultSet.getString("Active");

                    Masina masina = new Masina(marca, model, an_fabricatie, posesor ,active);
                    masini.add(masina);
                }
            }
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
        return masini;
    }
    public static void insertMasini(String marca, String model, String an_fabricatie, int posesor) {
        Connection conn = getConnection();

        try (CallableStatement callableStatement = conn.prepareCall("{CALL InsertMasiniProcedure(?, ?, ?, ?)}")) {
            callableStatement.setString(1, marca);
            callableStatement.setString(2, model);
            callableStatement.setString(3, an_fabricatie);
            callableStatement.setInt(4, posesor);

            callableStatement.executeUpdate();
            System.out.println("Mașină inserată cu succes.");
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
    }
    public static void deleteMasinaByMarcaProcedure(String marca) {
        Connection conn = getConnection();

        try (CallableStatement callableStatement = conn.prepareCall("{CALL DeleteMasinaByMarca(?)}")) {
            callableStatement.setString(1, marca);
            callableStatement.executeUpdate();
            System.out.println("");
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
    }


    public static List<Personal> getAllActivePersonal() {
        Connection conn = getConnection();
        List<Personal> personal_lista = new ArrayList<>();

        try (CallableStatement callableStatement = conn.prepareCall("{CALL GetAllActivePersonal()}")) {
            try (ResultSet resultSet = callableStatement.executeQuery()) {
                while (resultSet.next()) {
                    int clientId = resultSet.getInt("PersonalId");
                    String nume = resultSet.getString("Nume");
                    String prenume = resultSet.getString("Prenume");
                    String functie = resultSet.getString("Functie");
                    String active = resultSet.getString("Active");

                    Personal personal = new Personal(nume, prenume, functie, active);
                    personal_lista.add(personal);
                }
            }
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
        return personal_lista;
    }
    public static void insertPersonal(String nume, String prenume, String functie) {
        Connection conn = getConnection();
        try (CallableStatement callableStatement = conn.prepareCall("{CALL InsertPersonal(?, ?, ?)}")) {
            callableStatement.setString(1, nume);
            callableStatement.setString(2, prenume);
            callableStatement.setString(3, functie);

            callableStatement.executeUpdate();
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
    }
    public static void deletePersonalbyName(String nume) {
        Connection conn = getConnection();

        try (CallableStatement callableStatement = conn.prepareCall("{CALL DeletePersonalByName(?)}")) {
            callableStatement.setString(1, nume);
            callableStatement.executeUpdate();
            System.out.println(nume + " a fost concediat.");
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            closeConnection(conn);
        }
    }



}
