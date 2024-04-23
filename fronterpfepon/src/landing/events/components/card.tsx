import React from 'react';

const EventCard = () => {
    return (
        <div className='py-3'>
            <div className="bg-gray-100 shadow-xl rounded-lg flex items-center w-full">
                <div>
                    <img
                        src="https://daisyui.com/images/stock/photo-1606107557195-0e29a4b5b4aa.jpg"
                        alt="Shoes"
                        className="w-full h-64 object-cover"
                    />
                </div>

                <div className="p-6  flex">
                    <div className='w-11/12'>
                        <div className=''>
                            <h2 className="text-xl font-semibold mb-2">Titulo del Evento</h2>
                            <p className="text-gray-600">Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto.
                                Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500,
                                cuando un impresor</p>
                        </div>
                        <div className='flex items-center'>
                            <h3 className='m-2'>$---</h3>
                            <h3 className='m-2'> 01/01/2024  - 02/01/2024 </h3>
                        </div>
                    </div>
                    <div className='w-1/12 p-4 block items-end'>
                        <h3>...</h3>
                        <h3>Estado</h3>
                    </div>
                </div>

            </div>
        </div>

    );
};

export default EventCard;

