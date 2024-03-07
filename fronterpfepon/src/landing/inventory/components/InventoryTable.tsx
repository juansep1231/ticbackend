// InventoryTable.tsx
import React from 'react';
import StatusBadge from '../../../components/Generic/StatusBadge';

// Define TypeScript interface for inventory item
interface InventoryItem {
  id: number;
  producto: string;
  cantidad: number;
  categoria: string;
  precioUnitario: number;
  etiqueta: string;
  estado: string;
}

const InventoryTable: React.FC = () => {
  // Sample data for the inventory
  const inventoryData: InventoryItem[] = [
    {
      id: 1,
      producto: 'Producto 1',
      cantidad: 10,
      categoria: 'Categoría A',
      precioUnitario: 100,
      etiqueta: 'Etiqueta 1',
      estado: 'No Disponible'
    },
    {
      id: 1,
      producto: 'Producto 1',
      cantidad: 10,
      categoria: 'Categoría A',
      precioUnitario: 100,
      etiqueta: 'Etiqueta 1',
      estado: 'Disponible'
    },
    {
      id: 1,
      producto: 'Producto 1',
      cantidad: 10,
      categoria: 'Categoría A',
      precioUnitario: 100,
      etiqueta: 'Etiqueta 1',
      estado: 'Disponible'
    },
    // Add more inventory items here
  ];

  // Calculate precioTotal for each item
  const enrichedData = inventoryData.map(item => ({
    ...item,
    precioTotal: item.cantidad * item.precioUnitario,
  }));

  return (
    <div>
      <table className="min-w-full table-auto border">
        <thead >
          <tr>
            <th className="text-left px-4 py-2" > Producto </th>
            < th className="text-left px-4 py-2" > Cantidad </th>
            < th className="text-left px-4 py-2" > Categoría </th>
            < th className="text-left px-4 py-2" > Precio Unitario </th>
            < th className="text-left px-4 py-2" > Precio Total </th>
            < th className="text-left px-4 py-2" > Etiqueta </th>
            < th className="text-left px-4 py-2" > Estado </th>
          </tr>
        </thead>
        < tbody className="divide-y" >
          {
            enrichedData.map((item) => (
              <tr key={item.id} >
                <td className="px-4 py-2" > {item.producto} </td>
                < td className="px-4 py-2" > {item.cantidad} </td>
                < td className="px-4 py-2" > {item.categoria} </td>
                < td className="px-4 py-2" > {item.precioUnitario} </td>
                < td className="px-4 py-2" > {item.precioTotal} </td>
                < td className="px-4 py-2" > {item.etiqueta} </td>
                < td className="px-4 py-2" >
                  <StatusBadge
                    status={item.estado === 'Disponible' ? 1 : 0}
                    text={item.estado}
                  />
                </td>
              </tr>
            ))
          }
        </tbody>
      </table>
    </div>
  );
};

export default InventoryTable;
