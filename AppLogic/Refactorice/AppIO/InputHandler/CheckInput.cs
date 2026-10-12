using System;

namespace App.Logic.IO.Handler
{

    public static class CheckInput
    {

        // Centralizar patrones.
        private static readonly string[] ForbiddenPatterns = { "*", "%", "LIKE %", "LIKE *", ">", "<" };

        #region CheckInyection => Check for forbidden strings

        public static bool Injection(string input)
        {
            if (string.IsNullOrEmpty(input)) 
                return false;

            for (int i = 0; i < ForbiddenPatterns.Length; i++)
            {
                if (input.Contains(ForbiddenPatterns[i]))
                    return true;
            }
            return false;
        }

        #endregion

        /* IA Help
        /// <summary>
        /// Evalúa si el texto contiene caracteres o patrones prohibidos.
        /// </summary>
        public static bool IsDangerous(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            // Retorna TRUE inmediatamente si encuentra cualquier coincidencia
            return ForbiddenPatterns.Any(pattern => input.Contains(pattern));
        }
        */

    }
}
