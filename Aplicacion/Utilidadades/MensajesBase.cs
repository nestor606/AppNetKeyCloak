using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Utilidadades
{
    public  static class MensajesBase
    {
        public enum Satisfactorio
        {
            [Description("Insertado correctamente")]
            Insertado,
            [Description("Actualizado correctamente")]
            Actualizado,
            [Description("Eliminado correctamente")]
            Eliminado
        }

        public enum Error
        {
            [Description("No se pudo insertar, verifique que los datos estén correctos o comuníquese con el área de TI")]
            Insertar,
            [Description("No se pudo actualizar, verifique que los datos estén correctos o comuníquese con el área de TI")]
            Actualizar,
            [Description("No se pudo eliminar, verifique que los datos estén correctos o comuníquese con el área de TI")]
            Eliminar
        }

        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
