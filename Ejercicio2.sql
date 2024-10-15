--Crear un query con el que se obtenga una lista de facturas entre el año 2013 y 2015 
--en donde se muestre el id de la factura, el id del cliente, nombre del cliente, 
--el nombre del método de la entrega, el límite de crédito (en caso de que sea nulo, mostrar valor -1) 
--y el nombre del vendedor.

SELECT 
    I.InvoiceID AS 'ID Factura',
    C.CustomerID AS 'ID Cliente',
    C.CustomerName AS 'Nombre del Cliente',
    DM.DeliveryMethodName AS 'Método de Entrega',
    ISNULL(C.CreditLimit, -1) AS 'Límite de Crédito',
    P.FullName AS 'Nombre del Vendedor'
FROM 
    Sales.Invoices I
JOIN 
    Sales.Customers C ON I.CustomerID = C.CustomerID
JOIN 
    Application.DeliveryMethods DM ON I.DeliveryMethodID = DM.DeliveryMethodID
JOIN 
    Application.People P ON I.ContactPersonID = P.PersonID
WHERE 
    I.InvoiceDate BETWEEN '2013-01-01' AND '2015-12-31';


--Crear un query con el que se obtenga la cantidad de facturas por cliente,
--ordenado por esta cantidad, más una columna que indique el consecutivo 
--basado en esa cantidad; esta consulta como resultado deberá tener 4 columnas: 
--el id del cliente, nombre del cliente, total facturas, orden.

SELECT 
    C.CustomerID AS 'IdCliente',
    C.CustomerName AS 'NombreCliente',
    COUNT(I.InvoiceID) AS 'TotalFacturas',
    ROW_NUMBER() OVER (ORDER BY COUNT(I.InvoiceID) DESC) AS 'Orden'
FROM 
    Sales.Invoices I
JOIN 
    Sales.Customers C ON I.CustomerID = C.CustomerID
GROUP BY 
    C.CustomerID, C.CustomerName
ORDER BY 
    'TotalFacturas' DESC;
